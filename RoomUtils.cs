using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
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
    public class RoomUtils
    {
        public static Room GetRoomByPoint (Document doc, XYZ pt)
        {
            List<Room> rooms = GetRooms(doc);
            foreach (Room item in rooms)
            {
                if(item.IsPointInRoom(pt))
                    return item;
            }
            return null;
        }
       /*
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
       
        */
        public static List<Room> GetRooms(Document doc)
        {
            List<Room> rooms = new FilteredElementCollector(doc)
               .OfCategory(BuiltInCategory.OST_Rooms)
               .Cast<Room>()
               .ToList();
            return rooms;        }

    }
}
