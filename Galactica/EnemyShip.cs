﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galactica
{
    class EnemyShip : Ship
    {

        public bool MovingRight;
        public bool MovingForward;
        public int ForwardCounter;


        public static Random EnemyRand = new Random();


        public int ChanceToFire;
        public int EnemyLevel;
        public override void Initialize(Texture2D texture, Vector2 position)
        {

            Texture = texture;

            MovingRight = (EnemyRand.Next(0, 2) == 0) ? true : false;   // 50/50 of starting moving right or left

            //MovingRight = true;
            MovingForward = false;
            ForwardCounter = 0;
            // Set the starting position of the player around the middle of the screen and to the back

            Position = position;

            

            // Set the player to be active

            Active = true;



            // Set the player health

            Health = 100;



            // Speed in which the PlayerShip moves side to side

            StrafeSpeed = 3;


            // Speed in which PlayerShip moves up and down

            LateralSpeed = 4;


            ReloadSpeed = 50f;

            BulletSpeed = 10;
            

            Reloading = false;

            CurrentFire = TimeSpan.FromSeconds(60f / ReloadSpeed);

            LastFire = TimeSpan.Zero;

            ChanceToFire = 25;


        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Everything




            if (Position.Y < 0) Position.Y += LateralSpeed;
            else
            {
                if (Reloading == false)
                {
                    Fire();
                    Reloading = true;
                    LastFire = gameTime.TotalGameTime;
                }
            }

            Reload(gameTime);
            
            if (MovingRight)
            {
                if (MovingForward)
                {
                    if (ForwardCounter >= 16)
                    {
                        MovingRight = false;
                        MovingForward = false;
                        ForwardCounter = 0;

                    }
                    else
                    {
                        ForwardCounter++;
                        Position.Y += LateralSpeed;
                    }
                   
                }
                else if (Position.X > 431)
                {

                    MovingForward = true;
                    //MovingRight = false;

                }
                else
                {
                   
                    Position.X += StrafeSpeed;
                }
            }
            else
            {
                if (MovingForward)
                {
                    if (ForwardCounter >= 16)
                    {
                        MovingRight = true;
                        MovingForward = false;
                        ForwardCounter = 0;

                    }
                    else
                    {
                        ForwardCounter++;
                        Position.Y += LateralSpeed;
                    }
                }
                else if (Position.X < 5)
                {
                    MovingForward = true;
                }
                else
                {
                    Position.X -= StrafeSpeed;
                }
            }
        }

        //public Texture2D GetEnemyColor()
        //{
        //    switch (this.EnemyLevel)
        //    {
        //        case 1:
        //            return 
        //    }

                



        //}

        public override void Fire()
        {
            Random firePerc = new Random();
            int randPerc = firePerc.Next(1, 100);

            if (randPerc <= ChanceToFire)
            {
                var currentBullet1 = new EnemyBullet();

                currentBullet1.Initialize(Game1.enemyBulletTexture,
                    new Vector2(Position.X + 24, Position.Y + 64), new Quaternion(0, 0, 0, 0),
                    BulletSpeed);

                Game1.enemyBulletVolley.Add(currentBullet1);
            }
        }

        public override void Reload(GameTime gameTime)
        {
            if (!Reloading)
            {
                return;
            }
            if (gameTime.TotalGameTime - LastFire > CurrentFire)
            {
                Reloading = false;
            }
        }
    }
}
