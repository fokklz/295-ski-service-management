# -----------------------------------------------------------------------------
# This script is used to generate the README.md file from the RawREADME.md
# file and the class files. The class files are searched for a placeholder
# line that contains the class name. When found, the class definition is
# extracted and replaces the placeholder line. with a code block
# Author:  Fokko Vos
# Date:    02.12.2023
# -----------------------------------------------------------------------------

$workingDirectory = Get-Location

$classFilesDirectory = $workingDirectory
$inputFile = Join-Path $workingDirectory "RawREADME.md"
$outputFile = Join-Path $workingDirectory "README.md"

$directories = @{
    "COREMODEL" = "Models"
    "MODEL" = "Models\Base"
    "EFMODEL" = "Models\EF"
    "BSONMODEL" = "Models\BSON"
    "DTO" = "DTOs"
    "REQDTO" = "DTOs\Requests"
    "RESDTO" = "DTOs\Responses"
    "INTERFACE" = "Interfaces"
    "ENUM" = "Enums"
}

# Clear any old output
Clear-Content $outputFile -ErrorAction SilentlyContinue

# Function to extract class definition from a file
# Will also take care of codeblock wrapping
function Extract-ClassDefinition {
    param (
        [string]$Name,
        [string]$Type
    )

    $targetDir = Join-Path $workingDirectory $directories[$Type]
    $filePath = Join-Path $targetDir "$Name.cs"

    $classContent = @()
    $insideClass = $false
    $bracesCount = 0

    switch ($Type) {
		"INTERFACE" {
			$pattern = '^\s*public interface\s'
		}
		"ENUM" {
			$pattern = '^\s*public enum\s'
		}
		default {
			$pattern = '^\s*public class\s'
		}
	}

    Write-Host "Extracting $Type definition for $Name from $filePath"
    Get-Content $filePath | ForEach-Object {
        if ($_ -match $pattern) {
            $insideClass = $true
            $bracesCount = 1
            # Check and remove the first level of indentation (tab or 4 spaces)
            $classContent += if ($_ -match "^\t") { $_.Remove(0, 1) } 
                            elseif ($_ -match "^ {4}") { $_.Remove(0, 4) } 
                            else { $_ }
            return
        }

        if ($insideClass) {
            # Check and remove the first level of indentation (tab or 4 spaces)
            $line = if ($_ -match "^\t") { $_.Remove(0, 1) } 
                    elseif ($_ -match "^ {4}") { $_.Remove(0, 4) } 
                    else { $_ }

            $classContent += $line

            $line.ToCharArray() | ForEach-Object {
                if ($_ -eq '{') {
                    $bracesCount++
                }
                if ($_ -eq '}') {
                    $bracesCount--
                }
            }

            if ($bracesCount -eq 1) {
                $insideClass = $false
            }
        }
    }


    $codeBlockStart = '```csharp'
    $codeBlockEnd = '```'
    $codeBlockInner = $classContent -join "`r`n"

    if($Type -eq 'EFMODEL' -or $Type -eq 'BSONMODEL' ){
        return "$codeBlockStart`r`n$codeBlockInner`r`n$codeBlockEnd`r`n"
    }else{
        return "[back up](#contents-)`r`n$codeBlockStart`r`n$codeBlockInner`r`n$codeBlockEnd`r`n"
    }
}

$content = Get-Content $inputFile

foreach ($line in $content) {
    if ($line -match '<<(.+)::(.+)>>') {
        $type = $matches[1]
        $className = $matches[2]

        $line = Extract-ClassDefinition -Name $className -Type $type
    }

    $line | Out-File $outputFile -Append
}

Write-Host "README.md generated at $outputFile"