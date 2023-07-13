package dc

import (
	"github.com/uptrace/bun"
	"time"
)

type Domain struct {
	bun.BaseModel `bun:"table:domains,alias:dom"`
	Id            int        `bun:"id,pk,autoincrement" json:"id"`
	Name          string     `bun:"name" json:"name"`
	Type          DomainType `bun:"type" json:"type"`
	Created       time.Time  `bun:"created" json:"created"`
	Active        bool       `bun:"active" json:"active"`
}

type DomainType int

const (
	Infra   DomainType = 1
	Website DomainType = 2
)
