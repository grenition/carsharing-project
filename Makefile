up-dev:
	docker-compose -p f-project-dev -f deployments/docker-compose.yml -f  deployments/docker-compose.dev.yml up --build

up-prod:
	docker-compose -p f-project -f deployments/docker-compose.yml up --build -d

down-dev:
	docker-compose -p f-project-dev -f deployments/docker-compose.yml -f  deployments/docker-compose.dev.yml down
	
down-prod:
	docker-compose -p f-project -f deployments/docker-compose.yml down