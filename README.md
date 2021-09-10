# dotnet-microservices

# package for the Project
 dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection

 dotnet add package Microsoft.EntityFrameworkCore --version 5.0.9

 dotnet add package Microsoft.EntityFrameworkCore.Design

 dotnet add package Microsoft.EntityFrameworkCore.InMemory

 dotnet add package Microsoft.EntityFrameworkCore.SqlServer

  # create a docker image 
 docker build -t danhuideng/platformservice

 # launch the docker iamge in port 8080
 docker run -d -p 8080:80 danhuideng/platformservice

# list of all running containers
docker ps
 # kill the process run by the docker container

docker stop container_id

 # kubernetes content switch from aks to local docker-desktop

kubectl config get-contexts

kubectl config use-context docker-for-desktop