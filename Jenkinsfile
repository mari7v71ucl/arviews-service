pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh 'dotnet build'
            }
        }
	stage('Run tests') {
            steps {
                echo 'dotnet test'
            }
        }
	stage('Inject configuration file') {
            steps {
                configFileProvider(
			[configFile(fileId: '23bcc9a5-1873-4046-981a-4b0b297877f5', targetLocation: './arviews-service.API/appsettings.json')]) {
			echo 'appsettings.json was added' 
		}
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