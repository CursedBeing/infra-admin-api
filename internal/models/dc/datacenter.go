package dc

import (
	"github.com/uptrace/bun"
	"time"
)

type Datacenter struct {
	bun.BaseModel  `bun:"table:datacenters,alias:dc"`
	Id             int64     `json:"id" bun:",pk,autoincrement"`
	Name           string    `json:"name"`
	ContactName    string    `json:"contact_name"`
	SupportEmail   string    `json:"support_email"`
	SupportPhone   string    `json:"support_phone"`
	PaymentLink    string    `json:"payment_link"`
	IsExternal     bool      `json:"is_external"`
	AdditionalText string    `json:"additional_text"`
	Contract       string    `json:"contract"`
	Location       string    `json:"location"`
	Created        time.Time `json:"created"`
	Updated        time.Time `json:"updated"`
	VMs            []*Vm     `json:"virtual_machines" bun:"rel:has-many,join:id=dcid"`
}
