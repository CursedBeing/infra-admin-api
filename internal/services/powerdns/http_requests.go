package powerdns

import (
	"bytes"
	"encoding/json"
	"infra-api/internal/config"
	"infra-api/internal/models/dc"
	"io"
	"log"
	"net/http"
)

func RegisterARecord(data any, endpoint string) bool {
	//Загружаем конфигурацию
	config := config.Config{}
	config = config.LoadConfig()

	//Сериализуем данные в JSON
	jsonData, err := json.Marshal(data)
	if err != nil {
		log.Println("ERROR:", err.Error())
		return false
	}
	//Создаем запрос и указываем параметры
	req, err := http.NewRequest("PATCH", endpoint, bytes.NewBuffer(jsonData))
	req.Header.Set("Content-Type", "application/json")
	req.Header.Set("X-API-Key", config.PowerdnsApiKey)
	log.Println("DEBUG:", req)
	if err != nil {
		return false
	}

	//Выполняем запрос
	client := &http.Client{}
	resp, err := client.Do(req)
	if err != nil {
		log.Println("ERROR:", err.Error())
	}
	defer func(Body io.ReadCloser) {
		_ = Body.Close()
	}(resp.Body)
	respBody, err := io.ReadAll(resp.Body)
	if resp.Status != "204 No Content" {
		log.Println("PDNS API Error:", resp.Status, " | ", string(respBody))
		return false
	}
	return true
}
func RemoveARecord(data any, endpoint string) error {
	//Загружаем конфигурацию
	config := config.Config{}
	config = config.LoadConfig()

	//Сериализуем данные в JSON
	jsonData, err := json.Marshal(data)
	if err != nil {
		log.Println("ERROR:", err.Error())
		return err
	}

	//Создаем запрос и указываем параметры
	req, err := http.NewRequest("PATCH", endpoint, bytes.NewBuffer(jsonData))
	req.Header.Set("Content-Type", "application/json")
	req.Header.Set("X-API-Key", config.PowerdnsApiKey)
	log.Println("DEBUG:", req)
	if err != nil {
		return err
	}

	//Выполняем запрос
	client := &http.Client{}
	resp, err := client.Do(req)
	if err != nil {
		log.Println("ERROR:", err.Error())
		return err
	}
	defer func(Body io.ReadCloser) {
		_ = Body.Close()
	}(resp.Body)
	respBody, err := io.ReadAll(resp.Body)
	if resp.Status != "204 No Content" {
		log.Println("PDNS API Error:", resp.Status, " | ", string(respBody))
		return err
	}
	return nil
}
func AddVmToPDNS(vm dc.Vm) {
	//Готовим модель
	record := RecordList{
		Content:  vm.IpAddress,
		Disabled: false,
	}

	records := []RecordList{}
	records = append(records, record)

	sets := Rrset{
		Name:       vm.Name + "." + vm.Domain + ".",
		Type:       "A",
		Ttl:        3600,
		ChangeType: "REPLACE",
		Records:    records,
	}

	rrsets := Rrsets{}
	rrsets.Sets = append(rrsets.Sets, sets)

	//Отправляем запрос
	config := config.Config{}
	config = config.LoadConfig()
	RegisterARecord(rrsets, config.PowerdnsEndpoint+"/api/v1/servers/localhost/zones/"+vm.Domain+".")
}
func RemoveVmToPDNS(vm dc.Vm) {
	//Готовим модель
	sets := Rrset{
		Name:       vm.Name + "." + vm.Domain + ".",
		Type:       "A",
		ChangeType: "DELETE",
	}

	rrsets := Rrsets{}
	rrsets.Sets = append(rrsets.Sets, sets)

	//Отправляем запрос
	config := config.Config{}
	config = config.LoadConfig()
	_ = RemoveARecord(rrsets, config.PowerdnsEndpoint+"/api/v1/servers/localhost/zones/"+vm.Domain+".")
}
