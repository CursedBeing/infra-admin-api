package powerdns

import (
	"bytes"
	"encoding/json"
	"infra-api/internal/config"
	"infra-api/internal/models/dc"
	"infra-api/internal/models/powerdns"
	"io"
	"log"
	"net/http"
)

func RegisterARecord(data any, endpoint string) bool {
	//Загружаем конфигурацию
	cfg := config.Config{}
	cfg = cfg.LoadConfig()

	//Сериализуем данные в JSON
	jsonData, err := json.Marshal(data)
	if err != nil {
		log.Println("ERROR:", err.Error())
		return false
	}
	//Создаем запрос и указываем параметры
	req, err := http.NewRequest("PATCH", endpoint, bytes.NewBuffer(jsonData))
	req.Header.Set("Content-Type", "application/json")
	req.Header.Set("X-API-Key", cfg.PowerDNS.ApiKey)
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
	var cfg config.Config
	cfg = cfg.LoadConfig()

	//Сериализуем данные в JSON
	jsonData, err := json.Marshal(data)
	if err != nil {
		log.Println("ERROR:", err.Error())
		return err
	}

	//Создаем запрос и указываем параметры
	req, err := http.NewRequest("PATCH", endpoint, bytes.NewBuffer(jsonData))
	req.Header.Set("Content-Type", "application/json")
	req.Header.Set("X-API-Key", cfg.PowerDNS.ApiKey)
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
	record := powerdns.RecordList{
		Content:  vm.IpAddress,
		Disabled: false,
	}

	var records []powerdns.RecordList
	records = append(records, record)

	var sets = powerdns.Rrset{
		Name:       vm.Name + "." + vm.Domain.Name + ".",
		Type:       "A",
		Ttl:        3600,
		ChangeType: "REPLACE",
		Records:    records,
	}

	rrsets := powerdns.Rrsets{}
	rrsets.Sets = append(rrsets.Sets, sets)

	//Отправляем запрос
	var cfg config.Config
	cfg = cfg.LoadConfig()
	RegisterARecord(rrsets, cfg.PowerDNS.Endpoint+"/api/v1/servers/localhost/zones/"+vm.Domain.Name+".")
}
func RemoveVmToPDNS(vm dc.Vm) {
	//Готовим модель
	sets := powerdns.Rrset{
		Name:       vm.Name + "." + vm.Domain.Name + ".",
		Type:       "A",
		ChangeType: "DELETE",
	}

	rrsets := powerdns.Rrsets{}
	rrsets.Sets = append(rrsets.Sets, sets)

	//Отправляем запрос
	var cfg config.Config
	cfg = cfg.LoadConfig()
	_ = RemoveARecord(rrsets, cfg.PowerDNS.Endpoint+"/api/v1/servers/localhost/zones/"+vm.Domain.Name+".")
}
