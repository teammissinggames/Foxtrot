﻿using System;
using Microsoft.Xna.Framework.Graphics;

namespace Foxtrot.Models
{
    public class Animation
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; set; }

        public int FrameHeight { get { return Texture.Height; } }

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

        public float FrameSpeed { get; set; }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.16f;

        }
    }
}
