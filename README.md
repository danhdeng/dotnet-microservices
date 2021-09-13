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

# to deploy the image to kubernetes

kubectl apply -f platforms-deploy.yaml

# to list all the deployments in the kubernetes

kubectl get deployment

# to check the pods log

kubectl describe pod {pod id}

# upload the docker image to docker hub

docker push {image name}

# trust the self signed certifcate

dotnet dev-certs https --trust

https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide

# restart the deployment
bubectl rollout restart deployment {deployment name}

# Ingress nginx

https://kubernetes.github.io/ingress-nginx/deploy/

# ingress nginx for docker desktop

https://kubernetes.github.io/ingress-nginx/deploy/#docker-desktop

# ingress nginx for docker desktop deployment

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.0/deploy/static/provider/cloud/deploy.yaml

# get all kubernetes namespace

kubectl get namespace

# get all pods running for ingress-nginx

 kubectl get pods --namespace=ingress-nginx

# get all services under namespace ingress-nginx

kubectl get services --namespace=ingress-nginx
# get persistent volume claim

kubectl get pvc

# create secrete for mssql database access with name mssql and key P@55w0rd

kubectl create secret generic mssql --from-literal=SA_PASSWORD=P@55w0rd


