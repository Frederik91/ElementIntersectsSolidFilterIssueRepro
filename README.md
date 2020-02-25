# Prerequisites
* Revit (Version &ge; 2019), repro created in Revit 2019.2
* [.Net Core](https://dotnet.microsoft.com/download) version &ge; 2.0
* A way to run an external command, ie [Add-In Manager](https://knowledge.autodesk.com/support/revit-products/getting-started/caas/screencast/Main/Details/f62848c4-66fb-4ccd-8d74-0626e80c42d5.html)

# Repro steps

1. Open Project.rvt
2. Build ElementIntersectsSolidFilterIssue.csproj using `dotnet build`.
3. Run the external command in `bin\Debug\ElementIntersectsSolidFilterIssue.dll`
4. A Task Dialog will appear showing count of intersecting rooms. This count should be one, but for some reason the room is not detected as an intersecting element.