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
    public class DuctsUtils
    {
       /*
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
       */

        public static List<DuctType> GetDuctTypes(Autodesk.Revit.DB.Document doc)
        {
            List<DuctType> airChannelTypes = new FilteredElementCollector(doc)
               .OfClass(typeof(DuctType))
               .Cast<DuctType>()
               .ToList();
            return airChannelTypes;
        }

        public static List<MEPSystemType> GetDuctSystemTypes(Autodesk.Revit.DB.Document doc)
        {
            List<MEPSystemType> systemTypes = new FilteredElementCollector(doc)
                                       .OfClass(typeof(MEPSystemType))
                                       .Cast<MEPSystemType>()
                                       .ToList();
            return systemTypes;
        }

        public static List<Duct> GetElements(Autodesk.Revit.DB.Document doc)
        {
            List<Duct> ducts = new FilteredElementCollector(doc)
                                       .OfClass(typeof(Duct))
                                       .Cast<Duct>()
                                       .ToList();
            return ducts;
        }

    }
}
