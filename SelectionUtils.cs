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
    public class SelectionUtils
    {
        public static Element PickObject(ExternalCommandData commandData, string message = "Выберите элемент")
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, message);
            var oElement = doc.GetElement(selectedObject);
            return oElement;
        }

        public static T GetObject<T>(ExternalCommandData commandData, string promptMessage)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Reference selectedObj = null;
            T elem;
            try
            {
                selectedObj = uidoc.Selection.PickObject(ObjectType.Element, promptMessage);
            }
            catch (Exception)
            {
                return default(T);
            }
            elem = (T)(object)doc.GetElement(selectedObj.ElementId);
            return elem;
        }

        public static List<Element> PickObjects(ExternalCommandData commandData, string message = "Выберите элементы")
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedObjects = uidoc.Selection.PickObjects(ObjectType.Element, message);
            List<Element> elementList = selectedObjects.Select(selectedObject => doc.GetElement(selectedObject)).ToList();
            return elementList;
        }

        public static List<XYZ> GetPoints(ExternalCommandData commandData, ObjectSnapTypes objectSnapTypes,
                            string promptMessage = "Выберите точки")
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            List<XYZ> points = new List<XYZ>();

            while (true)
            {
                XYZ pickedPoint = null;
                try
                {
                    pickedPoint = uidoc.Selection.PickPoint(objectSnapTypes, promptMessage);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
                {
                    break;
                }
                points.Add(pickedPoint);
            }

            return points;
        }
        
        public static List<XYZ> Get2Points(ExternalCommandData commandData, ObjectSnapTypes objectSnapTypes,
                            string promptMessage = "Выберите 2 точки")
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            List<XYZ> points = new List<XYZ>();

            while (points.Count < 2)
            {
                XYZ pickedPoint = null;
                try
                {
                    pickedPoint = uidoc.Selection.PickPoint(objectSnapTypes, promptMessage);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
                {
                    break;
                }
                points.Add(pickedPoint);
            }

            return points;
        }

        public static XYZ GetPoint(ExternalCommandData commandData,
                         ObjectSnapTypes objectSnapTypes, string promptMessage = "Выберите точку")
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

                XYZ pickedPoint = null;
                try
                {
                    pickedPoint = uidoc.Selection.PickPoint(objectSnapTypes, promptMessage);
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException ex)
                {
                return null;
                }

            return pickedPoint;
        }


        public static List<Wall> SelectWalls(ExternalCommandData commandData)
        {
            UIApplication uIApplication = commandData.Application;
            UIDocument uIDocument = uIApplication.ActiveUIDocument;
            Document document = uIDocument.Document;

            Selection currentSelection = uIDocument.Selection;

            List<Wall> walls = new List<Wall>();

            if (currentSelection.GetElementIds().Count < 1)
            {
                WallsUtils.WallsSelectionFilter wsf = new WallsUtils.WallsSelectionFilter();
                List<Reference> pickedElement;

                try
                {
                    pickedElement = currentSelection.PickObjects(ObjectType.Element, wsf, "Выберите стены").ToList();
                }
                catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                {
                    return null;
                }

                foreach (Reference element in pickedElement)
                {
                    Wall wall = document.GetElement(element) as Wall;
                    walls.Add(wall);
                }
            }
            else
            {
                var currentSelectionElementIDs = currentSelection.GetElementIds();
                walls = new FilteredElementCollector(document, currentSelectionElementIDs)
                    .WhereElementIsNotElementType()
                    .OfClass(typeof(Wall))
                    .Cast<Wall>()
                    .ToList();
            }

            return walls;
        }
    }
}
