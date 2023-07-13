package dc

import (
	"github.com/uptrace/bun"
	"time"
)

type Datacenter struct {
	bun.BaseModel  `bun:"table:datacenters,alias:dc"`
	Id             int64     `json:"id" bun:",pk,autoincrement"`
	Name           string    `json:"name"`
	ContactName    string    `json:"contact_name,omitempty"`
	SupportEmail   string    `json:"support_email,omitempty"`
	SupportPhone   string    `json:"support_phone,omitempty"`
	PaymentLink    string    `json:"payment_link,omitempty"`
	IsExternal     bool      `json:"is_external,omitempty"`
	AdditionalText string    `json:"additional_text,omitempty"`
	Contract       string    `json:"contract,omitempty"`
	Location       string    `json:"location,omitempty"`
	Created        time.Time `json:"created,omitempty"`
	Updated        time.Time `json:"updated,omitempty"`
	VMs            []*Vm     `json:"virtual_machines,omitempty" bun:"rel:has-many,join:id=dcid"`
}
