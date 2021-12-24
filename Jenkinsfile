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
				
				stage('DEPLOY') {
    					steps {
						echo "Deploying the project"
					          bat "\"${tool '2019'}\" TestProject1.sln /p:DeployOnBuild=true /p:DeployDefaultTarget=WebPublish /p:WebPublishMethod=FileSystem /p:SkipInvalidConfigurations=true /t:build /p:Configuration=Release /p:Platform=\"Any CPU\" /p:DeleteExistingFiles=True /p:publishUrl=c:\\inetpub\\wwwroot\\TestProject1"
					}
				}
				//stage ('push artifact') {
                    //steps {
                        
                        //zip dir: 'C:\\inetpub\\wwwroot\\TestProject1', exclude: '', glob: '', overwrite: true, zipFile: 'TestZIP.zip'
                        
                        //bat '''@ECHO OFF
                        //set Source_Folder=C:\\ProgramData\\Jenkins\\.jenkins\\workspace\\BuilTestDeploy\\TestZIP.zip
                        //set Dest_Folder=C:\\backup\\1
                        //set FileName= BCKP 
                        
                        //copy %Source_Folder%\\*%FileName%* %Dest_Folder%\\.'''			
                    //}
                //}
				
				stage('TEST') {
				        agent { label 'Dummy' } 
    					steps {
						echo "Auto Test"
					            bat '"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe" C:\\Users\\pc2\\source\\repos\\TestProject1\\TestProject1\\bin\\Debug\\net472\\TestProject1.dll'
					}
				}
				stage('RELEASE') {
				        
    					steps {
						echo "Auto Test"
					           // bat '"C:\\Program Files (x86)\\NUnit.org\\nunit-console\\nunit3-console.exe" C:\\Users\\pc2\\source\\repos\\TestProject1\\TestProject1\\bin\\Debug\\net472\\TestProject1.dll'
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
