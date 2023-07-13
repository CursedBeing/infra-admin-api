package prom

import (
	"context"
	"infra-api/internal/database"
	"infra-api/internal/models/dc"
	"log"
)

func GetVmFromDb() ([]dc.Vm, error) {
	ctx := context.Background()
	db := database.Database{}
	dbCtx := db.SqlConnect()
	var vms []dc.Vm
	err := dbCtx.NewSelect().
		Model(&vms).
		Where("in_monitoring = true").
		Scan(ctx)
	if err != nil {
		return nil, err
	}

	err = dbCtx.Close()
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
		return nil, err
	}
	return vms, nil
}
