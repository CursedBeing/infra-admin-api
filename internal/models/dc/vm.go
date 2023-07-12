package dc

import "github.com/uptrace/bun"

// Vm описывает модель виртуальной машины в датацентре или хосте виртуализации
type Vm struct {
	bun.BaseModel  `bun:"table:vms,alias:vms"`
	Id             int64  `json:"id" bun:",pk,autoincrement"`
	Name           string `json:"name"`
	Domain         string `json:"domain"`
	IpAddress      string `json:"ip_address"`
	AdditionalText string `json:"additional_text"`
	InMonitoring   bool   `json:"in_monitoring"`
	DatacenterId   int64  `json:"dcid" bun:"dcid"`
}

type VmFilter struct {
	Name         string
	IpAddress    string
	DatacenterId int64
}
