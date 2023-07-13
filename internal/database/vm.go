package database

import (
	"context"
	"github.com/jackc/pgx/v5"
	"infra-api/internal/config"
	"infra-api/internal/models/dc"
	"log"
)

func GetVmsFromDb() []dc.Vm {
	ctx := context.Background()
	db := new(Database)
	dbCtx := db.SqlConnect()
	list := make([]dc.Vm, 0)
	err := dbCtx.NewSelect().
		Model(&list).
		Relation("Domain").
		Scan(ctx)
	if err != nil {
		log.Println("SQL ERROR:", err.Error())
	}
	defer dbCtx.Close()
	return list
	//return []dc.Vm{}
}
func GetSingleVmFromDb(id int64) (dc.Vm, error) {
	/*ctx := context.Background()
	db := Database{}
	dbCtx := db.SqlConnect()
	var vm dc.Vm
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
	return vm, nil*/
	return dc.Vm{}, nil
}
func CreateVmInDb(vm dc.Vm) (dc.Vm, error) {
	/*ctx := context.Background()
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
	return vm, nil*/
	return dc.Vm{}, nil
}
func DeleteVmFromDb(vm dc.Vm) error {
	/*ctx := context.Background()
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
	return nil*/
	return nil
}
func FindVm(filter dc.VmFilter) ([]dc.Vm, error) {
	var cfg config.Config
	ctx := context.Background()
	cfg = cfg.LoadConfig()
	conn, _ := pgx.Connect(ctx, cfg.Core.Database)
	defer conn.Close(ctx)

	var list = make([]dc.Vm, 0)
	var fileds = "vms.id, vms.name, vms.ip_address, vms.additional_text, vms.in_monitoring, d.id, d.name, d.active, d.created, dc.id, dc.name "
	var statement1 = "SELECT " + fileds +
		"FROM vms " +
		"JOIN domains d ON d.id = vms.domain_id " +
		"JOIN datacenters dc ON dc.id = vms.dcid " +
		"WHERE vms.name LIKE $1 " +
		"UNION ALL "
	var statement2 = "SELECT " + fileds +
		"FROM vms " +
		"JOIN domains d ON d.id = vms.domain_id " +
		"JOIN datacenters dc ON dc.id = vms.dcid " +
		"WHERE vms.ip_address LIKE $2 "

	rows, err := conn.Query(context.Background(), statement1+statement2, filter.Name, filter.IpAddress)
	if err != nil {
		log.Println("QueryRow failed:\n", err)
		return nil, err
	}

	for rows.Next() {
		var vm dc.Vm
		err = rows.Scan(&vm.Id, &vm.Name, &vm.IpAddress, &vm.AdditionalText, &vm.InMonitoring,
			&vm.Domain.Id, &vm.Domain.Name, &vm.Domain.Active, &vm.Domain.Created,
			&vm.Datacenter.Id, &vm.Datacenter.Name)
		if err != nil {
			log.Println("rows.Next() error", err.Error())
		}
		list = append(list, vm)
	}
	return list, nil
}
func GetVmsFromDbV2() ([]dc.Vm, error) {
	var cfg config.Config
	var ctx = context.Background()
	cfg = cfg.LoadConfig()

	conn, _ := pgx.Connect(ctx, cfg.Core.Database)
	defer conn.Close(ctx)

	var list = make([]dc.Vm, 0)
	var statement = "SELECT vms.id, vms.name, vms.ip_address, vms.additional_text, vms.in_monitoring, " +
		"d.id, d.name, d.active, d.created, " +
		"dc.id, dc.name " +
		"FROM vms " +
		"JOIN domains d ON d.id = vms.domain_id " +
		"JOIN datacenters dc ON dc.id = vms.dcid"
	rows, err := conn.Query(context.Background(), statement)
	if err != nil {
		log.Println("QueryRow failed:\n", err)
		return nil, err
	}

	for rows.Next() {
		var vm dc.Vm
		err = rows.Scan(&vm.Id, &vm.Name, &vm.IpAddress, &vm.AdditionalText, &vm.InMonitoring,
			&vm.Domain.Id, &vm.Domain.Name, &vm.Domain.Active, &vm.Domain.Created,
			&vm.Datacenter.Id, &vm.Datacenter.Name)
		if err != nil {
			log.Println("rows.Next() error", err.Error())
		}
		list = append(list, vm)
	}

	return list, nil
}
