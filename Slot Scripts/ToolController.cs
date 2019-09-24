

public static class ToolController
{
    public enum Tool {
        None,
        Circles,
        Sectors,
        LinksCW,
        LinksCCW,
        Spin,
        Play
    }

    private static Tool selectedTool = Tool.None;

    public static void SetTool(Tool newTool){
        selectedTool = newTool;
    }

    public static Tool GetTool(){
        return selectedTool;
    }
}
