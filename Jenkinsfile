pipeline {
    agent any

    stages {

        stage('Inject configuration files') {
            steps {
                configFileProvider(
                    [configFile(fileId: '23bcc9a5-1873-4046-981a-4b0b297877f5', targetLocation: './arviews-service.API/appsettings.json')]) {
                    echo 'configuration file for the application was added' 
                }
                    configFileProvider(
                    [configFile(fileId: '3e212275-43d0-4f28-bce4-d4b4b54cb826', targetLocation: './arviews-service.IntegrationTests/appsettings.json')]) {
                    echo 'configuration file for integration test project has been added' 	            
                }                      
            }           
        }
        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }
	    stage('Run unit tests') {
            steps {
                sh 'dotnet test ./arviews-service.Tests/arviews-service.Tests.csproj'
            }
        }
        stage('Run integration tests') {
            steps {
                sh 'dotnet test ./arviews-service.IntegrationTests/arviews-service.IntegrationTests.csproj'
            }
        }	
	    stage('Publish') {
            steps {
                sh 'dotnet publish -c Release'
            }
        }
	    stage('Docker build') {
            steps {
                sh 'docker build -t marius300/arviews-service:$BUILD_NUMBER .'
            }
        }
	    stage('Push to Docker hub') {
            steps {
                sh 'docker login --username $REGISTRY_USER --password $REGISTRY_PASS'
                sh 'docker push marius300/arviews-service:$BUILD_NUMBER'
                sh 'docker logout'
            }
        }
    }
    post {
        success {
            slackSend color: "good", message: "Pipeline executed successfully"
        }
        failure {
            slackSend color: "danger", message: "Pipeline execution failed"
        }
    }
}