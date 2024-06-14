using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;


namespace RevitAPITrainingLibrary
{
    public class FamiliesInstancesUtils
    {
        public static FamilyInstance CreateFamilyInstance(
            ExternalCommandData commandData,
            FamilySymbol oFamSymb,
            XYZ insertionPoint,
            Level oLevel,
            string message = "Create family instance")
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            FamilyInstance familyInstance = null;
            //create family instance
            using (var t = new Transaction(doc, message))
            {
                t.Start();
                if (!oFamSymb.IsActive)
                {
                    oFamSymb.Activate();
                    doc.Regenerate();
                }
                familyInstance = doc.Create.NewFamilyInstance(
                                    insertionPoint,
                                    oFamSymb,
                                    oLevel,
                                    Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                t.Commit();
            }
            return familyInstance;
        }

        public static FamilyInstance CreateFamilyInstanceWithoutTransaction(
            ExternalCommandData commandData,
            FamilySymbol oFamSymb,
            XYZ insertionPoint,
            Level oLevel)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            FamilyInstance familyInstance = null;
            //create family instance
           
           if (!oFamSymb.IsActive)
                {
                    oFamSymb.Activate();
                    doc.Regenerate();
                }
            familyInstance = doc.Create.NewFamilyInstance(
                                    insertionPoint,
                                    oFamSymb,
                                    oLevel,
                                    Autodesk.Revit.DB.Structure.StructuralType.NonStructural);                
            return familyInstance;
        }

        public class SelectionFilter : ISelectionFilter
        {
            public bool AllowElement(Element elem)
            {
                return elem.GetType().Equals(typeof(FamilyInstance));
            }

            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }

    }
}
