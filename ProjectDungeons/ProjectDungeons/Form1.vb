Option Strict On
Option Explicit On

Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Audio
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.GamerServices
Imports Microsoft.Xna.Framework.Media

Public Class Form1
    

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
Public Class Game
    Inherits Microsoft.Xna.Framework.Game
    'Fields in our game graphic manager etc'
    Dim graphics As GraphicsDeviceManager
    Dim spriteBatch As SpriteBatch
    Dim MainCharacter As New PlayerCharacter
    Dim EnemyBall As New Enemy
    Dim BrickTemplate As New Wall
    Dim FloorTemplate As New Wall
    Dim colPositions As New System.Collections.Generic.List(Of Integer()) 'list of wall positions
    Dim colWalls As New System.Collections.Generic.List(Of Wall) 'defines the walls for the screen
    Dim colEnemies As New System.Collections.Generic.List(Of Enemy)
    Public Sub New()
        graphics = New GraphicsDeviceManager(Me)

    End Sub

    Protected Overrides Sub Initialize()
        'TODO: Add your initialization logic here'
        MyBase.Initialize()
    End Sub
    Public Function GetSpriteSheet(ByVal GD As GraphicsDevice, ByVal strPath As String) As Texture2D
        Dim textureStream As System.IO.Stream = New System.IO.StreamReader(strPath).BaseStream
        Dim Sheet As Texture2D = Texture2D.FromStream(GD, textureStream)
        Return Sheet
    End Function
    Protected Overrides Sub LoadContent()

        MainCharacter.strSpriteFile = Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\character.png"
        EnemyBall.strSpriteFile = Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\enemy.png"
        ' TODO: use this.Content to load your game content here
        MyBase.LoadContent()
        ' Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = New SpriteBatch(GraphicsDevice)
        'Load the texture'
        'We are using Stream since i couldn't find how to make content in VB'
        'Dim textureStream As System.IO.Stream = New System.IO.StreamReader(MainCharacter.strSpriteFile).BaseStream
        'Loading the texture'
        'MainCharacter.spriteSheet = Texture2D.FromStream(GraphicsDevice, textureStream)
        MainCharacter.spriteSheet = GetSpriteSheet(GraphicsDevice, MainCharacter.strSpriteFile)
        MainCharacter.ColTextures.Add("attack", GetSpriteSheet(GraphicsDevice, Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\Sword.png"))
        MainCharacter.colFrontStand.Add(New Rectangle(16, 0, 16, 32))
        MainCharacter.colFrontWalk.Add(New Rectangle(0, 0, 16, 32))
        MainCharacter.colFrontWalk.Add(New Rectangle(32, 0, 16, 32))
        MainCharacter.colRightStand.Add(New Rectangle(110, 0, 10, 32))
        MainCharacter.colRightWalk.Add(New Rectangle(97, 0, 11, 32))
        MainCharacter.colRightWalk.Add(New Rectangle(121, 0, 12, 32))

        MainCharacter.colBackStand.Add(New Rectangle(48, 0, 16, 32))
        MainCharacter.colBackWalk.Add(New Rectangle(64, 0, 16, 32))
        MainCharacter.colBackWalk.Add(New Rectangle(80, 0, 16, 32))

        MainCharacter.colLeftStand.Add(New Rectangle(148, 0, 9, 32))
        MainCharacter.colLeftWalk.Add(New Rectangle(134, 0, 12, 32))
        MainCharacter.colLeftWalk.Add(New Rectangle(159, 0, 11, 32))
        MainCharacter.colCurrentFrames = MainCharacter.colFrontStand
        MainCharacter.SetHeight(32)
        MainCharacter.Setwidth(16)
        MainCharacter.SetPositionX(100)
        MainCharacter.SetPositionY(100)
        'enemy
        EnemyBall.spriteSheet = GetSpriteSheet(GraphicsDevice, EnemyBall.strSpriteFile)
        EnemyBall.colFrontStand.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colFrontWalk.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colRightStand.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colRightWalk.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colBackStand.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colBackWalk.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colLeftStand.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colLeftWalk.Add(New Rectangle(0, 0, 11, 12))
        EnemyBall.colCurrentFrames = EnemyBall.colFrontStand
        EnemyBall.SetHeight(12)
        EnemyBall.Setwidth(11)
        EnemyBall.SetPositionX(150)
        EnemyBall.SetPositionY(150)
        colEnemies.Add(EnemyBall)

        'MainCharacter.vecPosition = New Vector2(100, 100)
        Dim i() As Integer
        'textureStream = New System.IO.StreamReader(Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\BricksFull.png").BaseStream
        'BrickTemplate.Texture = Texture2D.FromStream(GraphicsDevice, textureStream)
        BrickTemplate.Texture = GetSpriteSheet(GraphicsDevice, Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\BricksFull.png")
        BrickTemplate.SetCollision(BrickTemplate.Texture.Width, BrickTemplate.Texture.Height, 22, 24, 0, 0)
        i = New Integer() {0, 0}
        colPositions.Add(i)
        i = New Integer() {1, 0}
        colPositions.Add(i)
        i = New Integer() {2, 0}
        colPositions.Add(i)
        i = New Integer() {3, 0}
        colPositions.Add(i)
        i = New Integer() {4, 0}
        colPositions.Add(i)
        i = New Integer() {5, 0}
        colPositions.Add(i)
        i = New Integer() {6, 0}
        colPositions.Add(i)
        i = New Integer() {7, 0}
        colPositions.Add(i)
        i = New Integer() {8, 0}
        colPositions.Add(i)
        i = New Integer() {9, 0}
        colPositions.Add(i)
        i = New Integer() {10, 0}
        colPositions.Add(i)
        i = New Integer() {0, 1}
        colPositions.Add(i)
        i = New Integer() {0, 2}
        colPositions.Add(i)
        i = New Integer() {0, 3}
        colPositions.Add(i)
        i = New Integer() {0, 4}
        colPositions.Add(i)
        i = New Integer() {0, 5}
        colPositions.Add(i)
        i = New Integer() {0, 6}
        colPositions.Add(i)
        i = New Integer() {0, 7}
        colPositions.Add(i)
        i = New Integer() {0, 8}
        colPositions.Add(i)
        i = New Integer() {0, 9}
        colPositions.Add(i)
        i = New Integer() {0, 10}
        colPositions.Add(i)
        i = New Integer() {10, 1}
        colPositions.Add(i)
        i = New Integer() {10, 2}
        colPositions.Add(i)
        i = New Integer() {10, 3}
        colPositions.Add(i)
        i = New Integer() {10, 4}
        colPositions.Add(i)
        i = New Integer() {10, 5}
        colPositions.Add(i)
        i = New Integer() {10, 6}
        colPositions.Add(i)
        i = New Integer() {10, 7}
        colPositions.Add(i)
        i = New Integer() {10, 8}
        colPositions.Add(i)
        i = New Integer() {10, 9}
        colPositions.Add(i)
        i = New Integer() {1, 10}
        colPositions.Add(i)
        i = New Integer() {2, 10}
        colPositions.Add(i)
        i = New Integer() {3, 10}
        colPositions.Add(i)
        i = New Integer() {4, 10}
        colPositions.Add(i)
        i = New Integer() {5, 10}
        colPositions.Add(i)
        i = New Integer() {6, 10}
        colPositions.Add(i)
        i = New Integer() {7, 10}
        colPositions.Add(i)
        i = New Integer() {8, 10}
        colPositions.Add(i)
        i = New Integer() {9, 10}
        colPositions.Add(i)
        i = New Integer() {10, 10}
        colPositions.Add(i)
        i = New Integer() {3, 4}
        colPositions.Add(i)
        For Each intPair() As Integer In colPositions
            colWalls.Add(New Wall(BrickTemplate.Texture, BrickTemplate.Texture.Width, BrickTemplate.Texture.Height, BrickTemplate.recCollision.Width * intPair(0), BrickTemplate.recCollision.Height * intPair(1), 22, 24))
        Next
        'textureStream = New System.IO.StreamReader(Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\Floor.png").BaseStream
        'FloorTemplate.Texture = Texture2D.FromStream(GraphicsDevice, textureStream)
        FloorTemplate.Texture = GetSpriteSheet(GraphicsDevice, Convert.ToString(Directory.GetCurrentDirectory) + "\Game Assets\Floor.png")
        FloorTemplate.SetCollision(48, 56, 48, 56, 0, 0)
        
    End Sub
    Protected Overrides Sub UnloadContent()
        : MyBase.UnloadContent()
        'TODO: Unload any non ContentManager content here'
    End Sub
    Protected Overrides Sub Update(ByVal gameTime As Microsoft.Xna.Framework.GameTime)
        'Allows the game to exit'
        If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed Then
            Me.Exit()
        End If
        MainCharacter.update(colWalls)
        For Each EnemyMob As Enemy In colEnemies
            EnemyMob.Update(colWalls)
        Next
        'TODO: Add your update logic here'

        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(ByVal gameTime As Microsoft.Xna.Framework.GameTime)
        GraphicsDevice.Clear(Color.CornflowerBlue)
        'TODO: Add your drawing code here'
        'Draw the sprite'

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend)
        For x As Integer = 0 To 4
            For y As Integer = 0 To 4
                spriteBatch.Draw(FloorTemplate.Texture, New Rectangle(x * FloorTemplate.recCollision.Width, y * FloorTemplate.recCollision.Height, FloorTemplate.recCollision.Width, FloorTemplate.recCollision.Height), Color.White)
            Next
        Next
        MainCharacter.Draw(spriteBatch)
        'spriteBatch.Draw(MainCharacter.spriteSheet, MainCharacter.vecPosition, MainCharacter.colCurrentFrames.Item(MainCharacter.intCurrentFrame), Color.White)
        For Each EnemyMob As Enemy In colEnemies
            EnemyMob.Draw(spriteBatch)
            'spriteBatch.Draw(MainCharacter.spriteSheet, MainCharacter.vecPosition, MainCharacter.colCurrentFrames.Item(MainCharacter.intCurrentFrame), Color.White)
        Next
        
        For Each Brick As Wall In colWalls
            spriteBatch.Draw(Brick.Texture, New Rectangle(Brick.intPositionX, Brick.intPositionY, Brick.intTextureWidth, Brick.intTextureHeight), Color.White)
        Next
        spriteBatch.End()
        MyBase.Draw(gameTime)
    End Sub
    Public Function detectCollisions(ByRef character As PlayerCharacter, ByVal vecDestination As Vector2, ByRef colWalls As System.Collections.Generic.List(Of Wall)) As Boolean
        Dim RecTest As New Rectangle(Convert.ToInt32(MainCharacter.recCollision.X + vecDestination.X), Convert.ToInt32(MainCharacter.recCollision.Y + vecDestination.Y), MainCharacter.recCollision.Width, MainCharacter.recCollision.Height)
        For Each wall As Wall In colWalls
            If Not (RecTest.Bottom <= wall.recCollision.Top Or RecTest.Top >= wall.recCollision.Bottom Or RecTest.Left >= wall.recCollision.Right Or RecTest.Right <= wall.recCollision.Left) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub CheckCollision()

    End Sub
End Class
Module mdlMain
    Sub Main()
        : Using game As New Game
            : game.Run()
            : End Using
    End Sub
End Module
Public Class Wall
    Public Texture As Texture2D
    Public intTextureHeight As Integer
    Public intTextureWidth As Integer
    Public recCollision As Rectangle
    Public intPositionX As Integer
    Public intPositionY As Integer
    Public Sub New()

    End Sub
    Public Sub New(ByVal txTexture As Texture2D, ByVal intPixelsWide As Integer, ByVal intpixelsTall As Integer)
        Texture = txTexture
        intTextureHeight = intpixelsTall
        intTextureWidth = intPixelsWide
    End Sub
    Public Sub New(ByVal txTexture As Texture2D, ByVal intPixelsWide As Integer, ByVal intpixelsTall As Integer, ByVal intCollisionX As Integer, ByVal intcollisionY As Integer, ByVal intCollisionWide As Integer, ByVal intcollisionTall As Integer)
        Texture = txTexture
        intTextureHeight = intpixelsTall
        intTextureWidth = intPixelsWide
        recCollision.X = intCollisionX
        recCollision.Y = intcollisionY
        recCollision.Width = intCollisionWide
        recCollision.Height = intcollisionTall
        intPositionX = recCollision.X + recCollision.Width - Texture.Width
        intPositionY = recCollision.Y + recCollision.Height - Texture.Height
    End Sub
    Public Sub SetCollision(ByVal intTextureWide As Integer, ByVal intTextureTall As Integer, ByVal intCollisionWide As Integer, ByVal intCollisionTall As Integer, ByVal intCollisionX As Integer, ByVal intCollisionY As Integer)
        recCollision.X = intCollisionX
        recCollision.Y = intCollisionY
        recCollision.Width = intCollisionWide
        recCollision.Height = intCollisionTall
        intTextureWide = intTextureWide
        intTextureHeight = intTextureTall
        intPositionX = recCollision.X + recCollision.Width - Texture.Width
        intPositionX = recCollision.Y + recCollision.Height - Texture.Height
    End Sub
End Class
Public Class AnimationFrame
    Public recFrame As New Rectangle
    Public intAnimationTime As Integer
    Public intOffsetX As Integer 'the two offset integers are for effects that move with the character
    Public intOffsetY As Integer
    Public Sub New(ByVal Frame As Rectangle, Optional ByVal Time As Integer = 10, Optional ByVal OffsetX As Integer = 0, Optional ByVal OffsetY As Integer = 0)
        recFrame = Frame
        intAnimationTime = Time
        intOffsetX = OffsetX
        intOffsetY = OffsetY
    End Sub
End Class
Public Class SpecialEffect
    Public spriteSheet As Texture2D
    Public strSpriteFile As String
    Public colAnimationFrames As New System.Collections.Generic.List(Of AnimationFrame)
    Public intCurrentFrame As Integer = 0
    Public intFrames As Integer 'used for counting frames between animation, increments everytime the same frame is displayed
    'Public intFrameMax As Integer = 10 'the number of frames until the animation switches
    Public sngRotation As Single = 0 'the rotation of the sprite
    Public bolWallPierce As Boolean
    Public bolEnemyPierce As Boolean
    Public bolAllyPierce As Boolean
    Public sefWallCollision As SpecialEffect
    Public sefEnemyCollision As SpecialEffect
    Public sefAllyCollision As SpecialEffect
    Public intPushBack As Integer 'pushback on collisions in pixels
    Public bolMovesWithCharacter As Boolean
    'Public intOffsetX As Integer 'the two offset integers are for effects that move with the character
    'Public intOffsetY As Integer
    Public intSpeedX As Integer
    Public intSpeedY As Integer
    Public vecPosition As Vector2
    Public vecCollision As Vector2
    Public intCollisionWidth As Integer
    Public intCollisionHeight As Integer
    Public bolAnimationRepeat As Boolean
    Public bolDelete As Boolean = False
    Public strKey As String
    Public bolRotatesWithChar As Boolean 'if true the graphic should rotate based on the character's graphic
    Public bolLockPosition As Boolean = False 'locks character in place when used
    Public bolLockDirection As Boolean = False 'locks the characters facing when used
    Public intLockDuration As Integer = 0 'locks character for a specified number of screens
    

    Public Sub update(ByRef character As character, Optional ByVal colWalls As System.Collections.Generic.List(Of Wall) = Nothing, Optional ByVal colEnemies As System.Collections.Generic.List(Of Enemy) = Nothing)
        Dim intX As Integer
        Dim intY As Integer
        Dim bolMoveX As Boolean = True
        Dim bolMoveY As Boolean = True
        If bolMovesWithCharacter = False Then
            intX = Convert.ToInt32(vecPosition.X) + intSpeedX
            intY = Convert.ToInt32(vecPosition.Y) + intSpeedY
        Else
            intX = Convert.ToInt32(character.vecPosition.X) + intSpeedX + colAnimationFrames.Item(intCurrentFrame).intOffsetX
            intY = Convert.ToInt32(character.vecPosition.Y) + intSpeedY + colAnimationFrames.Item(intCurrentFrame).intOffsetY
        End If
        If colWalls Is Nothing = False And bolWallPierce = False Then
            If detectWallCollisions(intX, intY, New Vector2(intX, 0), colWalls) Then
                bolMoveX = False
            End If
            If detectWallCollisions(intX, intY, New Vector2(0, intY), colWalls) Then
                bolMoveY = False
            End If
        End If
        If bolMoveX Then
            SetPositionX(intX)
        End If
        If bolMoveY Then
            SetPositionY(intY)
        End If
        intFrames += 1
        If intFrames = colAnimationFrames.Item(intCurrentFrame).intAnimationTime Then
            intFrames = 0
            If intCurrentFrame = colAnimationFrames.Count - 1 Then
                If bolAnimationRepeat = False Then
                    bolDelete = True
                Else
                    intCurrentFrame = 0
                End If
            End If
            intCurrentFrame += 1
        End If
        If colEnemies Is Nothing = False Then
            For Each Enemy As Enemy In colEnemies

            Next
        End If
    End Sub
    Public Function detectEnemyCollisions(ByVal intX As Integer, ByVal intY As Integer, ByVal vecDestination As Vector2, ByRef colEnemies As System.Collections.Generic.List(Of Enemy)) As Boolean
        Dim RecTest As New Rectangle(Convert.ToInt32(vecCollision.X + vecDestination.X), Convert.ToInt32(vecCollision.Y + vecDestination.Y), intCollisionWidth, intCollisionHeight)
        For Each Enemy As Enemy In colEnemies
            If Not (RecTest.Bottom <= Enemy.recCollision.Top Or RecTest.Top >= Enemy.recCollision.Bottom Or RecTest.Left >= Enemy.recCollision.Right Or RecTest.Right <= Enemy.recCollision.Left) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function detectWallCollisions(ByVal intX As Integer, ByVal intY As Integer, ByVal vecDestination As Vector2, ByRef colWalls As System.Collections.Generic.List(Of Wall)) As Boolean
        Dim RecTest As New Rectangle(Convert.ToInt32(vecCollision.X + vecDestination.X), Convert.ToInt32(vecCollision.Y + vecDestination.Y), intCollisionWidth, intCollisionHeight)
        For Each wall As Wall In colWalls
            If Not (RecTest.Bottom <= wall.recCollision.Top Or RecTest.Top >= wall.recCollision.Bottom Or RecTest.Left >= wall.recCollision.Right Or RecTest.Right <= wall.recCollision.Left) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub SetPositionX(ByVal intXAddition As Integer)
        vecPosition.X = intXAddition
        vecCollision.X = intXAddition
    End Sub
    Public Sub SetPositionX(ByVal sngXAddition As Single)
        vecPosition.X = sngXAddition
        vecCollision.X = sngXAddition
    End Sub
    Public Sub SetPositionY(ByVal intYAddition As Integer)
        vecPosition.Y = intYAddition
        vecCollision.Y = intYAddition
    End Sub
    Public Sub AddAnimationFrame(ByVal Frame As Rectangle, Optional ByVal Time As Integer = 10, Optional ByVal OffsetX As Integer = 0, Optional ByVal OffsetY As Integer = 0)
        colAnimationFrames.Add(New AnimationFrame(Frame, Time, OffsetX, OffsetY))
    End Sub

    Public Sub SetPositionY(ByVal sngYAddition As Single)
        vecPosition.Y = sngYAddition
        vecCollision.Y = sngYAddition
    End Sub
    Public Sub Draw(ByRef SpriteBatch As SpriteBatch)
        Dim vecOrigin As Vector2 = New Vector2(Convert.ToSingle(colAnimationFrames(intCurrentFrame).recFrame.Width), Convert.ToSingle(colAnimationFrames(intCurrentFrame).recFrame.Height))
        SpriteBatch.Draw(spriteSheet, vecPosition, colAnimationFrames(intCurrentFrame).recFrame, Color.White, sngRotation, vecOrigin, 1.0, SpriteEffects.None, 0)
    End Sub
End Class
Public Class character
    Public spriteSheet As Texture2D
    Public strSpriteFile As String
    Public colFrontStand As New System.Collections.Generic.List(Of Rectangle)
    Public colFrontWalk As New System.Collections.Generic.List(Of Rectangle)
    Public colBackStand As New System.Collections.Generic.List(Of Rectangle)
    Public colBackWalk As New System.Collections.Generic.List(Of Rectangle)
    Public colLeftStand As New System.Collections.Generic.List(Of Rectangle)
    Public colLeftWalk As New System.Collections.Generic.List(Of Rectangle)
    Public colRightStand As New System.Collections.Generic.List(Of Rectangle)
    Public colRightWalk As New System.Collections.Generic.List(Of Rectangle)
    Public colCurrentFrames As System.Collections.Generic.List(Of Rectangle)
    Public intCurrentFrame As Integer
    Public intFrames As Integer 'used for counting frames between animation, increments everytime the same frame is displayed
    Public intFrameMax As Integer = 10 'the number of frames until the animation switches
    Public dblSpeed As Double = 2
    Private timer As Stopwatch
    Public vecPosition As Vector2
    Public intHeight As Integer
    Public intWidth As Integer
    Public bolWalkLeft As Boolean = False
    Public bolWalkRight As Boolean = False
    Public bolWalkUp As Boolean = False
    Public bolWalkDown As Boolean = False
    Public strFacing As String 'which way your character is facing
    Public recCollision As Rectangle
    Public intKnockBackX As Integer = 0 'when character is hit these x and y values get added and start decrementing to zero with successive screens.
    Public intKnockBackY As Integer = 0
    Public colSpecialEffects As New System.Collections.Generic.Dictionary(Of String, SpecialEffect)
    Public ColTextures As New System.Collections.Generic.Dictionary(Of String, Texture2D)
    Public Function DiceRoll(ByVal intDiceNum As Integer, ByVal intDiceSize As Integer) As Integer
        Randomize()
        Dim intRoll As Integer = 0
        ' This function returns the result of a simulated die or dice roll 
        Dim i As Integer
        For i = 1 To intDiceNum
            intRoll += Convert.ToInt32(((intDiceSize - 1) * Rnd()) + 1)
        Next
        Return intRoll
    End Function
    Public Sub SetHeight(ByVal intNewHeight As Integer)
        intHeight = intNewHeight
        recCollision.Height = intNewHeight
    End Sub
    Public Sub Setwidth(ByVal intNewWidth As Integer)
        intWidth = intNewWidth
        recCollision.Width = intNewWidth
    End Sub
    
    Public Sub ChangeAnimationFrame(ByVal colFrames As System.Collections.Generic.List(Of Rectangle))

        If colFrames(0).X = colCurrentFrames(0).X And colFrames(0).Y = colCurrentFrames(0).Y Then
            If intFrames < intFrameMax Then
                intFrames += 1
            Else
                intFrames = 0
                If intCurrentFrame + 1 >= colFrames.Count Then
                    intCurrentFrame = 0
                Else
                    intCurrentFrame += 1
                End If
            End If
        Else
            colCurrentFrames = colFrames
            intCurrentFrame = 0
            intFrames = 0
        End If
    End Sub
    Public Function Walk() As Vector2
        Dim vecDestination As Vector2
        Dim dblX As Double
        Dim dblY As Double
        Dim dblSquareRoot As Double
        Dim bolLockDirection As Boolean = False
        'If dblX = 0 And dblY = 0 Then
        'ChangeAnimationFrame(colFrontStand)
        'End If
        For Each Sef As SpecialEffect In colSpecialEffects.Values
            If Sef.bolLockDirection Then
                bolLockDirection = True
            End If
        Next
        If bolWalkUp Then
            dblY += -1
            If bolWalkLeft = False And bolWalkRight = False And bolLockDirection = False Then
                ChangeAnimationFrame(colBackWalk)
            End If
        End If
        If bolWalkDown Then
            dblY += 1
            If bolWalkLeft = False And bolWalkRight = False And bolLockDirection = False Then
                ChangeAnimationFrame(colFrontWalk)
            End If
        End If
        If bolWalkLeft Then
            dblX += -1
            If bolLockDirection = False Then
                ChangeAnimationFrame(colLeftWalk)
            End If
        End If
        If bolWalkRight Then
            dblX += 1
            If bolLockDirection = False Then
                ChangeAnimationFrame(colRightWalk)
            End If
        End If
        If dblX = 0 Then
            vecDestination.X = 0
            vecDestination.Y = Convert.ToSingle(dblY * dblSpeed)
        ElseIf dblY = 0 Then
            vecDestination.X = Convert.ToSingle(dblX * dblSpeed)
            vecDestination.Y = 0
        Else
            dblSquareRoot = System.Math.Sqrt(dblSpeed)
            vecDestination.X = Convert.ToSingle(dblX * dblSquareRoot)
            vecDestination.Y = Convert.ToSingle(dblY * dblSquareRoot)
        End If
        Return vecDestination
    End Function
    Public Sub SetPositionX(ByVal intpositionX As Integer)
        vecPosition.X = intpositionX
        recCollision.X = intpositionX
    End Sub
    Public Sub SetPositionY(ByVal intpositionY As Integer)
        vecPosition.Y = intpositionY
        recCollision.Y = intpositionY
    End Sub
    Public Sub Update(Optional ByVal colWalls As System.Collections.Generic.List(Of Wall) = Nothing)
        Call UpdateCharacter(colWalls)
    End Sub
    Public Sub UpdateCharacter(Optional ByVal colWalls As System.Collections.Generic.List(Of Wall) = Nothing) 'all update functions inherited from Character class should call this
        If intKnockBackX > 0 Then
            intKnockBackX = Convert.ToInt32(intKnockBackX / 4) - 1
            If intKnockBackX < 0 Then
                intKnockBackX = 0
            End If
        End If
        If intKnockBackY > 0 Then
            intKnockBackY = Convert.ToInt32(intKnockBackY / 4) - 1
            If intKnockBackY < 0 Then
                intKnockBackY = 0
            End If
        End If
        For Each Sef As SpecialEffect In colSpecialEffects.Values
            Sef.update(Me, colWalls)
        Next
    End Sub
    Public Sub ObjectCleanup()
        Dim colKeys As New System.Collections.Generic.List(Of String)
        For Each Sef As SpecialEffect In colSpecialEffects.Values
            If Sef.bolDelete Then
                colKeys.Add(Sef.strKey)
            End If
        Next
        For Each strKey As String In colKeys
            colSpecialEffects.Remove(strKey)
        Next
    End Sub
    Public Function detectCollisions(ByVal vecDestination As Vector2, ByRef colWalls As System.Collections.Generic.List(Of Wall)) As Boolean
        Dim RecTest As New Rectangle(Convert.ToInt32(recCollision.X + vecDestination.X), Convert.ToInt32(recCollision.Y + vecDestination.Y), recCollision.Width, recCollision.Height)
        For Each wall As Wall In colWalls
            If Not (RecTest.Bottom <= wall.recCollision.Top Or RecTest.Top >= wall.recCollision.Bottom Or RecTest.Left >= wall.recCollision.Right Or RecTest.Right <= wall.recCollision.Left) Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Sub Draw(ByRef SpriteBatch As SpriteBatch)
        SpriteBatch.Draw(spriteSheet, vecPosition, colCurrentFrames.Item(intCurrentFrame), Color.White)
        For Each Sef As SpecialEffect In colSpecialEffects.Values
            If Sef.bolDelete = False Then
                Sef.Draw(SpriteBatch)
            End If
        Next
    End Sub
End Class
Public Class PlayerCharacter
    Inherits character
    Dim keyMoveUp As Keys = Keys.W
    Dim keyMoveDown As Keys = Keys.S
    Dim keyMoveRight As Keys = Keys.D
    Dim keyMoveLeft As Keys = Keys.A
    Dim keyAttack As Keys = Keys.NumPad4
    Public Sub CheckButtons()
        If Keyboard.GetState.IsKeyDown(keyMoveUp) Then
            bolWalkUp = True
        Else
            bolWalkUp = False
        End If
        If Keyboard.GetState.IsKeyDown(keyMoveLeft) Then
            bolWalkLeft = True
        Else
            bolWalkLeft = False
        End If
        If Keyboard.GetState.IsKeyDown(keyMoveDown) Then
            bolWalkDown = True
        Else
            bolWalkDown = False
        End If
        If Keyboard.GetState.IsKeyDown(keyMoveRight) Then
            bolWalkRight = True
        Else
            bolWalkRight = False
        End If
        If Keyboard.GetState.IsKeyDown(keyAttack) Then
            If colSpecialEffects.ContainsKey("attack") = False Then
                Dim AttackObj As New SpecialEffect
                AttackObj.bolMovesWithCharacter = True
                'AttackObj.AddAnimationFrame(New Rectangle(0, 0, 13, 13), 3, -10, -8)
                'AttackObj.AddAnimationFrame(New Rectangle(14, 0, 16, 13), 3, -18, -2)
                'AttackObj.AddAnimationFrame(New Rectangle(32, 0, 18, 13), 3, -21, 6)
                'AttackObj.AddAnimationFrame(New Rectangle(51, 0, 17, 13), 3, -17, 22)
                'AttackObj.AddAnimationFrame(New Rectangle(69, 0, 13, 13), 3, -10, 30)
                AttackObj.AddAnimationFrame(New Rectangle(0, 0, 13, 13), 3, 7, 2)
                AttackObj.AddAnimationFrame(New Rectangle(14, 0, 16, 13), 3, -1, 8)
                AttackObj.AddAnimationFrame(New Rectangle(32, 0, 18, 13), 3, -4, 16)
                AttackObj.AddAnimationFrame(New Rectangle(51, 0, 17, 13), 3, -1, 32)
                AttackObj.AddAnimationFrame(New Rectangle(69, 0, 13, 13), 3, 7, 40)
                AttackObj.spriteSheet = ColTextures.Item("attack")
                AttackObj.intSpeedX = 0
                AttackObj.intSpeedY = 0
                AttackObj.bolWallPierce = True
                AttackObj.bolAllyPierce = True
                AttackObj.bolEnemyPierce = True
                AttackObj.strKey = "attack"
                AttackObj.bolLockPosition = True
                AttackObj.bolLockDirection = True
                colSpecialEffects.Add("attack", AttackObj)
            End If
        End If
    End Sub
    Public Overloads Sub update(ByRef colWalls As System.Collections.Generic.List(Of Wall))
        UpdateCharacter(colWalls)
        updatePlayerCharacter(colWalls)
    End Sub
    Public Overloads Sub updatePlayerCharacter(ByRef colWalls As System.Collections.Generic.List(Of Wall))
        Dim testvector As Vector2
        Dim bolLockPosition As Boolean = False
        CheckButtons()
        testvector = Walk()
        For Each Sef As SpecialEffect In colSpecialEffects.Values
            If Sef.bolLockPosition Then
                bolLockPosition = True
            End If
        Next
        If bolLockPosition = False Then
            If detectCollisions(New Vector2(testvector.X, 0), colWalls) = False Then
                SetPositionX(Convert.ToInt32(vecPosition.X) + Convert.ToInt32(testvector.X))
            End If
            If detectCollisions(New Vector2(0, testvector.Y), colWalls) = False Then
                SetPositionY(Convert.ToInt32(vecPosition.Y) + Convert.ToInt32(testvector.Y))
            End If
        End If
        ObjectCleanup()
    End Sub

End Class
Public Class Enemy
    Inherits character
    Public intAIDelay As Integer = 0
    Public strDirection As String = "none"
    Public Overloads Sub Update(ByRef colWalls As System.Collections.Generic.List(Of Wall))
        UpdateCharacter(colWalls)
        UpdateEnemy(colWalls)
    End Sub
    Public Sub UpdateEnemy(ByRef colWalls As System.Collections.Generic.List(Of Wall))
        Dim testvector As Vector2
        Dim intRoll As Integer
        intAIDelay += 1
        If intAIDelay = 10 Then
            intAIDelay = 0
            intRoll = DiceRoll(1, 4)
            If intRoll = 1 Then
                intRoll = DiceRoll(1, 10)
                Select Case intRoll
                    Case 1
                        strDirection = "right"
                    Case 2
                        strDirection = "left"
                    Case 3
                        strDirection = "up"
                    Case 4
                        strDirection = "down"
                    Case 5
                        strDirection = "right-up"
                    Case 6
                        strDirection = "left-up"
                    Case 7
                        strDirection = "right-down"
                    Case 8
                        strDirection = "left-down"
                    Case 9
                        strDirection = "none"
                End Select
            End If
        End If
        Select Case strDirection
            Case "right"
                bolWalkRight = True
                bolWalkLeft = False
                bolWalkDown = False
                bolWalkUp = False
            Case "left"
                bolWalkRight = False
                bolWalkLeft = True
                bolWalkDown = False
                bolWalkUp = False
            Case "up"
                bolWalkRight = False
                bolWalkLeft = False
                bolWalkDown = False
                bolWalkUp = True
            Case "down"
                bolWalkRight = False
                bolWalkLeft = False
                bolWalkDown = True
                bolWalkUp = False
            Case "right-up"
                bolWalkRight = True
                bolWalkLeft = False
                bolWalkDown = False
                bolWalkUp = True
            Case "left-up"
                bolWalkRight = False
                bolWalkLeft = True
                bolWalkDown = False
                bolWalkUp = True
            Case "right-down"
                bolWalkRight = True
                bolWalkLeft = False
                bolWalkDown = True
                bolWalkUp = False
            Case "left-down"
                bolWalkRight = False
                bolWalkLeft = True
                bolWalkDown = True
                bolWalkUp = False
            Case "none"
                bolWalkRight = False
                bolWalkLeft = False
                bolWalkDown = False
                bolWalkUp = False
        End Select
        testvector = Walk()
        If detectCollisions(New Vector2(testvector.X, 0), colWalls) = False Then
            SetPositionX(Convert.ToInt32(vecPosition.X) + Convert.ToInt32(testvector.X))
        End If
        If detectCollisions(New Vector2(0, testvector.Y), colWalls) = False Then
            SetPositionY(Convert.ToInt32(vecPosition.Y) + Convert.ToInt32(testvector.Y))
        End If
        ObjectCleanup()
    End Sub
End Class
