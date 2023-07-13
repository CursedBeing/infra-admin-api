package dc

// Vm описывает модель виртуальной машины в датацентре или хосте виртуализации
type Vm struct {
	Id             int64      `json:"id"`
	Name           string     `json:"name"`
	IpAddress      string     `json:"ip_address"`
	AdditionalText string     `json:"additional_text"`
	InMonitoring   bool       `json:"in_monitoring"`
	Datacenter     Datacenter `json:"datacenter,omitempty"`
	Domain         Domain     `json:"domain,omitempty"`
}

type VmFilter struct {
	Name      string `json:"name"`
	IpAddress string `json:"ip_address"`
}
