package config

import (
	"encoding/json"
	"os"
)

type Config struct {
	Db               string `json:"db"`
	ListenPort       string `json:"listenPort"`
	PowerdnsEndpoint string `json:"powerdns_endpoint"`
	PowerdnsApiKey   string `json:"powerdns_apikey"`
}

// LoadConfig Загружает конфигурацию из файла settings.json
func (conf Config) LoadConfig() Config {
	file, err := os.Open("./configs/settings.json")
	defer func(file *os.File) {
		_ = file.Close()
	}(file)

	decoder := json.NewDecoder(file)
	err = decoder.Decode(&conf)

	if err != nil {
		panic(err)
	}
	return conf
}
