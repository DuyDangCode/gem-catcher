using Godot;
using System;

public partial class Gem : Area2D
{
	private int _speed;
	[Signal]
	public delegate void OutOfScreenEventHandler();
	public override void _Ready()
	{
		_speed = 100;

	}

	public override void _PhysicsProcess(double delta)
	{
		Position += new Vector2(0, _speed * (float)delta);
	}




	public void _on_area_entered(Area2D area)
	{
		if (area.Name == "Paddle")
			RemoveGem();

	}

	public void RemoveGem()
	{
		SetPhysicsProcess(false);
		QueueFree();
	}

	public override void _Process(double delta)
	{
		if (Position.Y > GetViewportRect().Size.Y)
		{
			EmitSignal("OutOfScreen");
			RemoveGem();
		}
	}
}
