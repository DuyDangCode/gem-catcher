using Godot;
using System;

public partial class Paddle : Area2D
{

	private int _speed;
	public override void _Ready()
	{
		_speed = 500;
	}
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionPressed("move_left"))
		{
			Position -= new Vector2(_speed * (float)delta, 0);

		}
		if (Input.IsActionPressed("move_right"))
		{

			Position += new Vector2(_speed * (float)delta, 0);
		}
	}

	public override void _Process(double delta)
	{
	}
}
