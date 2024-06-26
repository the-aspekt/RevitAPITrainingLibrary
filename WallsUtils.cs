using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class WallsUtils
    {
        internal class WallsSelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                return elem.GetType().Equals(typeof(Wall));
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }

        public static List<WallType> GetTypes(Document doc)
        {
            var wallTypes =
                new FilteredElementCollector(doc)
                    .OfClass(typeof(WallType))
                    .Cast<WallType>()
                    .ToList();
            return wallTypes;
        }

        public static XYZ GetPointOnBaseOfWall(Wall wall, double coordinate = 0.5)
        {
            double c = coordinate != 0 ? coordinate % 1 : 0;           
            LocationCurve hostCurve = wall.Location as LocationCurve;
            XYZ p0 = hostCurve.Curve.GetEndPoint(0);
            XYZ p1 = hostCurve.Curve.GetEndPoint(1);
            XYZ targetPoint = p0 + (p1 - p0)*c;
            return targetPoint;
        }
        public static XYZ GetPointOnTopOfWall(Wall wall, double coordinate = 0.5)
        {
            double c = coordinate % 1;
            if(c == 0 && coordinate != 0)
                c = 1;
            LocationCurve hostCurve = wall.Location as LocationCurve;
            XYZ p0 = hostCurve.Curve.GetEndPoint(0);
            XYZ p1 = hostCurve.Curve.GetEndPoint(1);
            XYZ top = new XYZ(0, 0, wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble());
            XYZ targetPoint = p0 + (p1 - p0) * c + top;
            return targetPoint;
        }

    }
}
