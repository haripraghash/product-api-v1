param(
	[Parameter(Mandatory=$true)]
	[string]$ResourceGroupName,

	[Parameter(Mandatory=$true)]
	[string]$ResourceName,

	[Parameter(Mandatory=$true)]
	[string]$KeyVaultName,

	[string]$AppInsightsApiKeySecretName = 'appinsightsapikey',

	[string]$AppInsightsApiKeyPipelineVariable = 'appinsightsapikey'
)
$ErrorActionPreference = 'Stop'

function Create-AppInsightsApiKey()
{
    Write-Host "App insights api key does for app insights resource $ResourceName not exist. Creating new api key for azure devops."
	$apiKeyDescription="azuredevops"
	$permissions = @("WriteAnnotations")
	$appInsightsApiKey = New-AzureRmApplicationInsightsApiKey -ResourceGroupName $ResourceGroupName -Name $ResourceName -Description $apiKeyDescription -Permissions $permissions
	Write-Host "App insights api key created for resource $ResourceName"
    return $appInsightsApiKey

}

Write-Host "Checking to see if there is an api key already available"
$appInsightsApiKey = Get-AzureRmApplicationInsightsApiKey -ResourceGroupName $ResourceGroupName -Name $ResourceName | Where Description -EQ "azuredevops"

if(!$appInsightsApiKey)
{
	Write-Host "Api key does not exist"
	$appInsightsApiKey = Create-AppInsightsApiKey
}

$keyVault = Get-AzureRMKeyVault -VaultName $KeyVaultName

if(!$keyVault)
{
	Write-Error "Key vault $KeyVaultName does not exist"
}

$AppInsightsApiKeyPersistedSecret = (Get-AzureKeyVaultSecret -VaultName $KeyVaultName -Name $AppInsightsApiKeySecretName).SecretValueText


if(!$AppInsightsApiKeyPersistedSecret)
{
Write-Host "Api key secret $appInsightsApiKeySecret does not exist in vault $KeyVaultName."
Remove-AzureRmApplicationInsightsApiKey -ResourceGroupName $ResourceGroupName -Name $ResourceName -ApiKeyId $appInsightsApiKey.Id
$appInsightsApiKey = Create-AppInsightsApiKey
Write-Host "Storing App insights api key in key vault $KeyVaultName with secret name  $AppInsightsApiKeySecretName"
$appInsightsApiKeySecret = ConvertTo-SecureString -String $appInsightsApiKey.ApiKey -AsPlainText -Force
Set-AzureKeyVaultSecret -VaultName $KeyVaultName -Name $AppInsightsApiKeySecretName -SecretValue $appInsightsApiKeySecret
$AppInsightsApiKeyPersistedSecret = (Get-AzureKeyVaultSecret -VaultName $KeyVaultName -Name $AppInsightsApiKeySecretName).SecretValueText
Write-Host "Persisted App insights api key in key vault $KeyVaultName with secret name  $AppInsightsApiKeySecretName"
Write-Host "##vso[task.setvariable variable=$AppInsightsApiKeyPipelineVariable]$AppInsightsApiKeyPersistedSecret"
}
else{
Write-Host "Secret $AppInsightsApiKeySecretName is available in $KeyVaultName"
Write-Host "##vso[task.setvariable variable=$AppInsightsApiKeyPipelineVariable]$AppInsightsApiKeyPersistedSecret"
}


