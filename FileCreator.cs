public class FileCreator 
{
    private static string FOLDER_NAME = @"../../../templates/";
    public static void AddOn(string addOnName) {
        string fileData = "";
        string inputFile = string.Format("{0}add-on-template.cs", FOLDER_NAME);
        string outputFile = inputFile.Replace("add-on-template.cs", string.Format("{0}.cs", addOnName));
        using(StreamReader streamReader = new StreamReader(inputFile))
        {
            var windowClassName = string.Format("{0}Window", addOnName);
            fileData = streamReader.ReadToEnd();
            fileData = fileData.Replace("$itemName$", addOnName);
            fileData = fileData.Replace("$itemNameText$", "\"" + addOnName + "\"");
            fileData = fileData.Replace("$nt8menuText$", "\"" + addOnName + "\"");
            fileData = fileData.Replace("$nt8targetmenu$", "\"ControlCenterMenuItemNew\"");
            fileData = fileData.Replace("$nt8WindowClass$", windowClassName);
            fileData = fileData.Replace(string.Format("public {0}", windowClassName), string.Format("public {0}()", windowClassName));
            fileData = fileData.Replace("$mainMenuItemName$", "\"MainMenuItem\"");
            fileData = fileData.Replace("$nt8TabClassName$", string.Format("{0}Tab", addOnName));
            fileData = fileData.Replace("$nt8TabFactoryName$", string.Format("{0}TabFactory", addOnName));
            fileData = fileData.Replace("$xamlFileName$", string.Format("\"AddOns.{0}UI.xaml\"", addOnName));
        }

        
        if (File.Exists(outputFile))
        {
            File.Delete(outputFile);
        }
        File.WriteAllText(outputFile, fileData);
    }
}