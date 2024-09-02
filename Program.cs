using System.Runtime.CompilerServices;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class Program
{

  public static void Main()
  {
    MainProgram program = new MainProgram();

    while (!WindowShouldClose())
    {
      program.Update();
      program.Draw();
    }
    program.End();
  }

}

public class MainProgram
{
  private static int days = 0;
  private static int daysGone = 0;
  int rectSize = GetScreenHeight();

  public MainProgram()
  {
    string[] startDate = new string[3];
    string[] endDate = new string[3];
    string title = ""; 
    StreamReader sw = new StreamReader("Dates.txt");
    title = sw.ReadLine();
    for (int i = 0; i < 3; i++)
    {
      if (i < 2) startDate[i] = sw.ReadLine() + "/";
      else startDate[i] = sw.ReadLine();
    }
    for (int i = 0; i < 3; i++)
    {
      if (i < 2) endDate[i] = sw.ReadLine() + "/";
      else endDate[i] = sw.ReadLine();
    }
    sw.Close();

    TimeSpan timeDifference = DateTime.Parse(string.Join("", endDate)) - DateTime.Parse(string.Join("", startDate));
    days = timeDifference.Days > 0 ? timeDifference.Days : 0;

    timeDifference = DateTime.Now - DateTime.Parse(string.Join("", startDate));
    daysGone = timeDifference.Days;


    SetConfigFlags(ConfigFlags.ResizableWindow);
    InitWindow(800, 800, title);

    rectSize = GetScreenHeight();
  }

  public void Update()
  {
    if (IsWindowResized())
    {
      rectSize = GetScreenHeight();
    }
  }

  public void Draw()
  {
    BeginDrawing();
    ClearBackground(Color.Black);

    bool isColorChanged = false;
    int y = 0;
    int x = 0;
    Color rectColor = Color.Green;
    for (int i = 0; i < days; i++)
    {
      if (x * rectSize + rectSize > GetScreenWidth())
      {
        y++;
        x = 0;
      }

      if (i >= daysGone && !isColorChanged)
      {
        rectColor = Color.Red;
        isColorChanged = true;
      }

      DrawRectangleRec(new Rectangle(x * rectSize, y * rectSize, rectSize, rectSize), rectColor);
      DrawRectangleLinesEx(new Rectangle(x * rectSize, y * rectSize, rectSize, rectSize), 1, Color.White);
      x++;
    }

    if (y * rectSize + rectSize > GetScreenHeight())
    {
      rectSize--;
    }

    EndDrawing();
  }

  public void End()
  {
    CloseWindow();
  }

}