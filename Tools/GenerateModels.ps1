
dotnet tool restore
$OUTPUT_PATH = Join-Path $PSScriptRoot "..\Models\ContentTypes"
dotnet tool run KontentModelGenerator -o $OUTPUT_PATH -n "Planty.Models"