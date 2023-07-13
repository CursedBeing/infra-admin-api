package database

import (
	"database/sql"
	"github.com/uptrace/bun"
	"github.com/uptrace/bun/dialect/pgdialect"
	"github.com/uptrace/bun/driver/pgdriver"
	"infra-api/internal/config"
)

type Database struct {
	dsn string
}

// SqlConnect создает контекс подключения к БД с нужными параметрами
func (db Database) SqlConnect() *bun.DB {
	connStr := db.getConnectionString()
	conn := sql.OpenDB(pgdriver.NewConnector(pgdriver.WithDSN(connStr)))
	context := bun.NewDB(conn, pgdialect.New())
	return context
}

// Получает строку соединения с БД из файла settings.json
func (Database) getConnectionString() string {
	Configuration := config.Config{}
	Configuration = Configuration.LoadConfig()
	return Configuration.Core.Database
}
