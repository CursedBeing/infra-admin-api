package database

import (
	"context"
	"infra-api/internal/models/dc"
	"log"
	"time"
)

func GetDatacentersFromDb() []dc.Datacenter {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	var datacenters []dc.Datacenter
	err := dbCtx.NewSelect().
		Model(&datacenters).
		Relation("VMs").
		Order("created DESC").
		Scan(ctx)
	err = dbCtx.Close()
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
	}
	return datacenters
}
func CreateDatacenterInDb(datacenter dc.Datacenter) (dc.Datacenter, error) {
	ctx := context.Background()
	db := Database{}
	datacenter.Created = time.Now().Local()
	datacenter.Updated = datacenter.Created
	dbCtx := db.SqlConnect()
	_, err := dbCtx.NewInsert().Model(&datacenter).Exec(ctx, &datacenter)
	err = dbCtx.Close()
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
	}
	return datacenter, nil
}
func UpdateDatacenterInDb(datacenter dc.Datacenter) (dc.Datacenter, error) {
	ctx := context.Background()
	datacenter.Updated = time.Now().Local()
	db := Database{}
	dbCtx := db.SqlConnect()
	_, err := dbCtx.NewUpdate().
		Model(&datacenter).
		WherePK().
		Exec(ctx, &datacenter)
	err = dbCtx.Close()
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
	}
	return datacenter, nil
}
func DeleteDatacenterInDb(datacenter dc.Datacenter) error {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	_, err := dbCtx.NewDelete().
		Model(&datacenter).
		WherePK().
		Exec(ctx)
	err = dbCtx.Close()
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
	}
	return nil
}
