using Godot;
using System;

public partial class AnimationController : AnimatedSprite2D
{

	public Vector2 direction;
	// Called when the node enters the scene tree for the first time.
	public int facing = 1; //Right = 1, left = -1
	public string animation = "idle";

	public bool eating = false;

	void setXDirection()
	{
		float x = direction.X;
		// GD.Print(x);
		if(x > 0)
		{
			facing = 1;
			FlipH = false;
		}
		if(x < 0)
		{
			facing = -1;
			FlipH = true;
		}
	}

	void setWalking()
	{
		if (direction.Length() == 0)
		{
			animation = "idle";
		}
		else
		{
			animation = "walk";
		}
	}


	public override void _Ready()
	{
		Play("idle");
	}

	void playAnimation()
	{
		// GD.Print(animation);
		Play(animation);
	}

	void eat()
	{
		if(eating)
		{
			animation = "eat";
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		setXDirection();
		setWalking();
		eat();
		playAnimation();
	}
}
