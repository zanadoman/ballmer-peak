using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("Button").Pressed += OnStartButtonPressed;
	}

	private void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/world.tscn");
	}
}
