package database

import (
	"context"
	"infra-api/internal/models/dc"
	"log"
)

func GetVmsFromDb() []dc.Vm {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	vms := []dc.Vm{}
	err := dbCtx.NewSelect().
		Model(&vms).
		Scan(ctx)
	err = dbCtx.Close()
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
	}
	return vms
}
func GetSingleVmFromDb(id int64) (dc.Vm, error) {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	vm := dc.Vm{}
	vm.Id = id
	err := dbCtx.NewSelect().
		Model(&vm).
		WherePK().
		Scan(ctx)
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
		return vm, err
	}
	_ = dbCtx.Close()
	return vm, nil
}
func CreateVmInDb(vm dc.Vm) (dc.Vm, error) {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	_, err := dbCtx.NewInsert().
		Model(&vm).
		Exec(ctx)
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
		return vm, err
	}
	_ = dbCtx.Close()
	return vm, nil
}
func UpdateVmInDb(vm dc.Vm) (dc.Vm, error) {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	_, err := dbCtx.NewUpdate().
		Model(&vm).
		Exec(ctx)
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
		return vm, err
	}
	_ = dbCtx.Close()
	return vm, nil
}
func DeleteVmFromDb(vm dc.Vm) error {
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	_, err := dbCtx.NewDelete().
		Model(&vm).
		WherePK().
		Exec(ctx)
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
		return err
	}
	_ = dbCtx.Close()
	return nil
}
func FindVm(filter dc.VmFilter) ([]dc.Vm, error) {
	var list []dc.Vm
	ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()

	//Осуществляем поиск по имени
	if filter.Name != "" {
		var byName []dc.Vm
		_ = dbCtx.NewSelect().
			Model(&byName).
			Where("name LIKE ?", filter.Name).
			Limit(25).
			Scan(ctx)
		for i := range byName {
			list = append(list, byName[i])
		}
	}

	//Осуществляем поиск по имени
	if filter.IpAddress != "" {
		var byIp []dc.Vm
		_ = dbCtx.NewSelect().
			Model(&byIp).
			Where("ip_address LIKE ?", filter.IpAddress).
			Scan(ctx)
		for i := range byIp {
			list = append(list, byIp[i])
		}
	}

	return list, nil
}
