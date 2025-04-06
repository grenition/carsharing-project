up-dev:
	docker-compose -p f-project-dev -f docker-compose.yml -f docker-compose.dev.yml up --build

up-prod:
	docker-compose -p f-project -f docker-compose.yml up --build -d

down-dev:
	docker-compose -p f-project-dev down
	
down-prod:
	docker-compose -p f-project down