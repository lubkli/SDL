using System;
using SDL2;

namespace coreSdl
{
    class Program
    {
        static void Main(string[] args)
        {
            // SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
            // SDL.SDL_Quit();
            // Console.WriteLine("Hello World!");

            // Initialize the library.
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine("Unable to initialize SDL. Error: {0}", SDL.SDL_GetError());
                return;
            }

            // Create a window.
            IntPtr window = IntPtr.Zero;
            window = SDL.SDL_CreateWindow(".NET Core SDL2 Test",
                    SDL.SDL_WINDOWPOS_CENTERED,
                    SDL.SDL_WINDOWPOS_CENTERED,
                    1020,
                    800,
                    SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (window == IntPtr.Zero)
            {
                Console.WriteLine("Unable to create a window. SDL. Error: {0}", SDL.SDL_GetError());
                return;
            }

            // Create the renderer. This is the "canvas".
            IntPtr renderer = SDL.SDL_CreateRenderer(window, -1, 0);

            // Load the image texture.
            //var image = SDL.SDL_LoadBMP("Player.bmp");
            //var playerTexture = SDL.SDL_CreateTextureFromSurface(renderer, image);
            IntPtr playerTexture = SDL_image.IMG_LoadTexture(renderer, "Player.png");

            // Set renderer drawing color.
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

            // Clear the screen.
            SDL.SDL_RenderClear(renderer);

            // Set source rectangle.
            SDL.SDL_Rect sRect;
            sRect.x = 0;
            sRect.y = 0;
            sRect.w = 128;
            sRect.h = 128;

            // Set target rectangle.
            SDL.SDL_Rect tRect;
            tRect.x = 100;
            tRect.y = 100;
            tRect.w = 128;
            tRect.h = 128;
            
            // "Paint" the player to the canvas.
            SDL.SDL_RenderCopy(renderer, playerTexture, ref sRect, ref tRect);
            // SDL.SDL_RenderCopy(renderer, playerTexture, IntPtr.Zero, IntPtr.Zero);

            // Present the "Painting" (backbuffer) to the screen. Call this once per frame.
            SDL.SDL_RenderPresent(renderer);

            //SDL.SDL_Delay(5000);
            bool quit = false;

            SDL.SDL_Event e;

            while (!quit)
            {
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            quit = true;
                            break;

                        case SDL.SDL_EventType.SDL_WINDOWEVENT:
                            switch (e.window.windowEvent)
                            {
                                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_EXPOSED:
                                    SDL.SDL_RenderClear(renderer);
                                    SDL.SDL_RenderCopy(renderer, playerTexture, ref sRect, ref tRect);
                                    SDL.SDL_RenderPresent(renderer);
                                    break;
                            }
                            break;

                        case SDL.SDL_EventType.SDL_KEYDOWN:

                        switch (e.key.keysym.sym)
                        {
                            case SDL.SDL_Keycode.SDLK_q:
                                quit = true;
                                break;
                        }
                        break;
                    }
                }
            }

            SDL.SDL_DestroyTexture(playerTexture);
            //SDL.SDL_FreeSurface
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }
    }
}
