COMPOSE_BASE_FILE = deployments/docker-compose.yml
COMPOSE_DEV_FILE = deployments/docker-compose.dev.yml
PROJECT_DEV_NAME = f-project-dev
PROJECT_PROD_NAME = f-project

up-dev:
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) pull
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) up --build

up-dev-daemon:
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) pull
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) up --build -d

up-prod:
	docker-compose -p $(PROJECT_PROD_NAME) -f $(COMPOSE_BASE_FILE) pull
	docker-compose -p $(PROJECT_PROD_NAME) -f $(COMPOSE_BASE_FILE) up --build -d

down-dev:
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) down

down-prod:
	docker-compose -p $(PROJECT_PROD_NAME) -f $(COMPOSE_BASE_FILE) down

restart-dev: down-dev up-dev

restart-dev-daemon: down-dev up-dev-daemon

restart-prod: down-prod up-prod

psql-shell:
	@export $$(grep -v '^#' deployments/.env | xargs) && \
	CONTAINER=$$(docker ps --filter "name=f-project-dev_postgres" --format "{{.Names}}" | head -n 1) && \
	docker exec -it $$CONTAINER psql -U $$POSTGRES_USER -d $$POSTGRES_DB