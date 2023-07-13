package config

import (
	"encoding/json"
	"os"
)

type Config struct {
	PowerDNS PowerDNSConfig `json:"power_dns"`
	Core     CoreConfig     `json:"core"`
}

type CoreConfig struct {
	Listen   string `json:"listen"`
	Database string `json:"database"`
}
type PowerDNSConfig struct {
	Endpoint string `json:"endpoint"`
	ApiKey   string `json:"api_key"`
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
