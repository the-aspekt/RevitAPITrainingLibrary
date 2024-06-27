using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class PipesUtils
    {
        public static List<PipingSystemType> GetPipingSystems(Document doc)
        {
            List<PipingSystemType> pipingSystemTypes = new FilteredElementCollector(doc)
                                                        .OfClass(typeof(PipingSystemType))
                                                        .Cast<PipingSystemType>()
                                                        .ToList();
            return pipingSystemTypes;
        }
        public static List<Pipe> GetElements(Autodesk.Revit.DB.Document doc)
        {
            List<Pipe> pipes = new FilteredElementCollector(doc)
                                       .OfClass(typeof(Pipe))
                                       .Cast<Pipe>()
                                       .ToList();
            return pipes;
        }
    }
}
