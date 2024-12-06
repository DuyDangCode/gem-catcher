using Godot;
using System;

public partial class Game : Node2D
{
	private Label _scoreLable;
	private int _score = 0;
	private bool gameOver = false;
	private Timer _timer;
	private AudioStreamPlayer2D _audioStreamPlayer2D;
	private AudioStreamPlayer2D _addScoreAudio;
	private AudioStreamPlayer2D _gameOverAudio;

	[Export]
	public PackedScene gemScene;
	public override void _Ready()
	{
		_scoreLable = GetNode<Label>("ScoreLabel");
		_timer = GetNode<Timer>("Timer");
		_audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		_addScoreAudio = GetNode<AudioStreamPlayer2D>("AddScoreAudio");
		_gameOverAudio = GetNode<AudioStreamPlayer2D>("GameOverAudio");
		UpdateScoreLabel();
	}

	public void _on_gem_area_entered(Area2D area)
	{
		GD.Print(area);
	}

	public void _on_timer_timeout()
	{
		if (gameOver) return;
		var newGem = (Area2D)gemScene.Instantiate();
		var rand = new Random();
		newGem.Position = new Vector2((float)rand.NextDouble() * GetViewportRect().Size.X, 0);
		newGem.Connect("area_entered", Callable.From(new Action<Area2D>(AddScore)));
		newGem.Connect("OutOfScreen", Callable.From(EndGame));
		AddChild(newGem);
	}

	public void UpdateLevel(float time)
	{
		_timer.WaitTime -= time;
	}

	public void AddScore(Area2D area)
	{
		if (area.Name != "Paddle") return;
		_addScoreAudio.Play();
		_score++;
		if (_score % 10 == 0)
		{
			UpdateLevel(0.3f);
		}
		UpdateScoreLabel();
	}

	public void EndGame()
	{
		GD.Print("Game over");
		gameOver = true;
		_timer.Stop();
		_audioStreamPlayer2D.Stop();
		_gameOverAudio.Play();
		foreach (var child in GetChildren())
		{
			child.SetProcess(false);
			child.SetPhysicsProcess(false);
		}

	}

	public void UpdateScoreLabel()
	{
		_scoreLable.Text = $"Score: {_score}";
	}

	public override void _Process(double delta)
	{

	}
}
