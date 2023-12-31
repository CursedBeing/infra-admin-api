# infra-admin-api

## Описание
Бэкэнд приложение для учета и инвентаризации ресурсов ЦОД, таких как: Хосты виртуализации,
виртуальные машины и сетевые устройства. Также реализована интеграция с PowerDNS и Prometheus позволяющаяя:
1. регистрировать/удалить новые хосты, вм или свичи в системе своей системе DNS
2. Подключить Prometheus Service Discovery к эндроинту приложения: `http://ip:5000/api/v1/prom/vm`
чтобы получить список ВМ для дальнейшего сбора метрик

## Как запустить
1. Установить Docker
2. `git clone https://github.com/CursedBeing/infra-admin-api.git`
3. `cd infra-admin-api`
4. `docker build -t infra-admin-api:latest .`
5. `docker run -d  --name infra-api -p 5000:5000 -v conf:/app/configs infra-admin-api:latest`

## Дополнительно
Проект находится в стадии разработки и пока применяется для упрощения 
учета инфраструктурой разработчика

## Roadmap
1. Реализовать возможность указывать свои метки для мониторинга Prometheus.
2. Реализовать учет подсетей, выделяемых через сетевые устройства.
3. Реализовать аутентификацию и авторизацию .
4. Подготовить UI для работы с API.
5. И много другое что придет в голову разработчику.