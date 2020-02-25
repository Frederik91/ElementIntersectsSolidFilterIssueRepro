using System;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ElementIntersectsSolidFilterIssue
{
    public class ExternalCommandCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var document = commandData.Application.ActiveUIDocument.Document;

            try
            {
                var mass = new FilteredElementCollector(document)
                                .WhereElementIsNotElementType()
                                .OfCategory(BuiltInCategory.OST_Mass)
                                .FirstElement();

                var massSolid = GetSolid(mass);
                var intersectionFilter = new ElementIntersectsSolidFilter(massSolid);
                var intersectingRoomsFilter = new FilteredElementCollector(document)
                                            .OfCategory(BuiltInCategory.OST_Rooms)
                                            .WherePasses(intersectionFilter);

                TaskDialog.Show("Result", $"Found {intersectingRoomsFilter.GetElementCount()} intersecting rooms");
                return Result.Succeeded;
            }
            catch (Exception exception)
            {
                message = exception.Message + "\n" + exception.StackTrace;
                return Result.Failed;
            }
        }

        private Solid GetSolid(Element element)
        {
            if (!(element is FamilyInstance familyInstance))
                return null;
            var options = new Options();
            var geometryElement = familyInstance.get_Geometry(options);
            return geometryElement.OfType<Solid>().First(x => x.Volume > 0);
        }
    }
}
