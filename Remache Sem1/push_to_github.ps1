param(
	[string]$RemoteUrl = "",
	[string]$Branch = "main"
)

function ExecGit([string]$args) {
	Write-Host "git $args"
	$proc = Start-Process git -ArgumentList $args -NoNewWindow -Wait -PassThru -RedirectStandardError stderr.txt -RedirectStandardOutput stdout.txt
	if ($proc.ExitCode -ne 0) {
		Write-Error "Comando git falló: git $args"
		Get-Content stderr.txt
		exit $proc.ExitCode
	}
}

if (-not (Test-Path -Path ".git")) {
	Write-Host "Inicializando repositorio git..."
	ExecGit "init"
}

if ($RemoteUrl -ne "") {
	$existing = & git remote get-url origin 2>$null
	if ($LASTEXITCODE -eq 0) {
		Write-Host "Actualizando remote 'origin' -> $RemoteUrl"
		ExecGit "remote set-url origin $RemoteUrl"
	} else {
		Write-Host "Añadiendo remote 'origin' -> $RemoteUrl"
		ExecGit "remote add origin $RemoteUrl"
	}
}

Write-Host "Añadiendo archivos y creando commit..."
ExecGit "add -A"

try {
	ExecGit "commit -m \"Initial commit from local workspace\""
} catch {
	Write-Host "No hay cambios para commitear o ya existe un commit. Continuando..."
}

ExecGit "branch -M $Branch"

if ($RemoteUrl -eq "") {
	Write-Host "No se ha definido RemoteUrl. Si quieres pushear, ejecuta:`n .\push_to_github.ps1 -RemoteUrl 'https://github.com/yourname/yourrepo.git' -Branch main"
	exit 0
}

Write-Host "Empujando al remoto origin/$Branch..."
ExecGit "push -u origin $Branch"

Write-Host "Operación completada. Revisa el repositorio remoto en: $RemoteUrl"
