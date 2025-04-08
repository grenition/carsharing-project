COMPOSE_BASE_FILE = deployments/docker-compose.yml
COMPOSE_DEV_FILE = deployments/docker-compose.dev.yml
PROJECT_DEV_NAME = f-project-dev
PROJECT_PROD_NAME = f-project

up-dev:
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) pull
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) up --build

up-prod:
	docker-compose -p $(PROJECT_PROD_NAME) -f $(COMPOSE_BASE_FILE) pull
	docker-compose -p $(PROJECT_PROD_NAME) -f $(COMPOSE_BASE_FILE) up --build -d

down-dev:
	docker-compose -p $(PROJECT_DEV_NAME) -f $(COMPOSE_BASE_FILE) -f $(COMPOSE_DEV_FILE) down

down-prod:
	docker-compose -p $(PROJECT_PROD_NAME) -f $(COMPOSE_BASE_FILE) down
