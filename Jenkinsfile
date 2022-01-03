pipeline {
			agent any
			triggers {
                    githubPush()
                }
			stages {
				stage('GIT CHEKING'){
					steps{
						echo "Checking out from Git"
						git 'https://github.com/satishpwr/TestProject1.git'
					}
				}
				stage('RESTORE NUGET') {
    					steps {
						echo "Restoring Nuget Packages"
    					        bat 'C:\\ProgramData\\chocolatey\\lib\\NuGet.CommandLine\\tools\\nuget.exe restore "C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\BuilTestDeploy\\TestProject1.sln"'
					}
				}
				stage('BUILD') {
    					steps {
						echo "Building the project"
					            bat "\"${tool '2019'}\" TestProject1.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
					}
				}
				
				stage('DEV DEPLOY') {
    					steps {
						echo "Deploying the project"
					          bat "\"${tool '2019'}\" TestProject1.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=c:\\inetpub\\wwwroot\\TestProject11"
					}
				}
				stage ('BACKUPS') {
                    steps {
                        
                       
                        powershell '''$source = "C:\\inetpub\\wwwroot\\TestProject11"
                        $BuildNo = ${env:BUILD_NUMBER}
                        $destination = "C:\\inetpub\\backup\\TestProject11_$BuildNo.zip"
                        if(Test-path $destination) {Remove-item $destination}
                        Add-Type -assembly "system.io.compression.filesystem"
                        [io.compression.zipfile]::CreateFromDirectory($source, $destination)'''
                    }
                }
				
				stage('TEST') {
				        agent { label 'Dummy' } 
    					steps {
						echo "Auto Test"
					            bat '"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe" C:\\Users\\pc2\\source\\repos\\TestProject1\\TestProject1\\bin\\Debug\\net472\\TestProject1.dll'
					}
				}
                
				stage('QA RELEASE') {
				        
				        input {
                            message "Ready to deploy?"
                            ok "Approve"
                        }
				        
        				steps {
    						echo "Release"
    					    bat "\"${tool '2019'}\" TestProject1.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=c:\\inetpub\\wwwroot\\TestProject1"
    					}
				}
				
			}
			environment {
                    EMAIL_TO = 'satishpawar.vision@gmail.com'
                }
            post {
                    failure {
                        emailext body: 'Check console output at $BUILD_URL to view the results. \n\n ${CHANGES} \n\n -------------------------------------------------- \n${BUILD_LOG, maxLines=100, escapeHtml=false}', 
                                to: "${EMAIL_TO}", 
                                subject: 'Build failed in Jenkins: $PROJECT_NAME - #$BUILD_NUMBER'
                    }
                    unstable {
                        emailext body: 'Check console output at $BUILD_URL to view the results. \n\n ${CHANGES} \n\n -------------------------------------------------- \n${BUILD_LOG, maxLines=100, escapeHtml=false}', 
                                to: "${EMAIL_TO}", 
                                subject: 'Unstable build in Jenkins: $PROJECT_NAME - #$BUILD_NUMBER'
                    }
                    changed {
                        emailext body: 'Check console output at $BUILD_URL to view the results.', 
                                to: "${EMAIL_TO}", 
                                subject: 'Jenkins build is back to normal: $PROJECT_NAME - #$BUILD_NUMBER'
                    }
                }
            
}
