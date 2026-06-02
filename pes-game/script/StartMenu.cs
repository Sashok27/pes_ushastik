using Godot;

public partial class StartMenu : Control
{
	private Button _startButton;

	public override void _Ready()
	{
		_startButton = GetNode<Button>("StartButton");
		_startButton.Pressed += OnStartButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		// Укажите правильный путь к вашей сцене world.tscn
		GetTree().ChangeSceneToFile("res://scenes/world.tscn");
	}
}
