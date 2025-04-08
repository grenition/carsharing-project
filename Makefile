up-dev:
	docker-compose -p f-project-dev \
 		-f deployments/docker-compose.yml \
 		-f deployments/docker-compose.dev.yml \
 		up --build --pull --remove-orphans

up-prod:
	docker-compose -p f-project \
 		-f deployments/docker-compose.yml \
 		up --build --pull --remove-orphans -d

down-dev:
	docker-compose -p f-project-dev \
 		-f deployments/docker-compose.yml \
 		-f  deployments/docker-compose.dev.yml \
 		down
	
down-prod:
	docker-compose -p f-project \
	 	-f deployments/docker-compose.yml \
	 	down