Imports GTA
Imports INMNativeUI
Imports System
Imports System.Drawing
Imports System.IO
Imports GTA.Math

Public Class Bank
    Inherits Script

    ReadOnly _scaleform As New Scaleform("instructional_buttons")

    Dim password As String = ""
    Dim ticker As Integer = 0
    Dim atmModel As New List(Of Model) From {"prop_fleeca_atm", "prop_atm_01", "prop_atm_02", "prop_atm_03"}
    Dim playerCam, atmCam As Camera

    Private Sub LoadSettings()
        CreateFile()
        fcBank = config.GetValue(Of Long)("GENERAL", "Franklin", 0)
        mtBank = config.GetValue(Of Long)("GENERAL", "Michael", 0)
        tpBank = config.GetValue(Of Long)("GENERAL", "Trevor", 0)
        transFee = config.GetValue("SETTING", "TransferFee", 1)
        withFee = config.GetValue("SETTING", "WithdrawFee", 1)
        depFee = config.GetValue("SETTING", "DepositFee", 0)
        lostCashBusted = config.GetValue(Of Boolean)("SETTING", "LoseCashWhenBusted", False)
        lostCashDead = config.GetValue(Of Boolean)("SETTING", "LoseCashWhenDead", False)
        interest = config.GetValue(Of Single)("SETTING", "InterestRate", 0.0025F)
    End Sub

    Private Sub CreateFile()
        If Not File.Exists("scripts\ATM.ini") Then
            config.SetValue(Of Long)("GENERAL", "Franklin", 0)
            config.SetValue(Of Long)("GENERAL", "Michael", 0)
            config.SetValue(Of Long)("GENERAL", "Trevor", 0)
            config.SetValue("SETTING", "TransferFee", 1)
            config.SetValue("SETTING", "WithdrawFee", 1)
            config.SetValue("SETTING", "DepositFee", 0)
            config.SetValue(Of Boolean)("SETTING", "LoseCashWhenBusted", False)
            config.SetValue(Of Boolean)("SETTING", "LoseCashWhenDead", False)
            config.SetValue(Of Single)("SETTING", "InterestRate", 0.0025F)
            config.Save()
        End If
    End Sub

    Public Sub New()
        LoadSettings()
        btTrans = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 355), New Size(1200, 100), Color.LightPink)
        btDeposit = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 460), New Size(1200, 100), Color.LightPink)
        btWithdraw = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 565), New Size(1200, 100), Color.LightPink)
        btBalance = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 670), New Size(1200, 100), Color.LightPink)

        btFranklin = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 355), New Size(1200, 100), Color.LightPink)
        btMichael = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 460), New Size(1200, 100), Color.LightPink)
        btTrevor = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 565), New Size(1200, 100), Color.LightPink)

        bt1kd = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt5kd = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt10kd = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        bt50kd = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        bt100kd = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)
        bt500kd = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt1md = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt5md = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        bt10md = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        bt50md = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)
        bt100md = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt500md = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt1bd = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        btRemaind = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        btCustomd = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)

        bt1kw = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt5kw = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt10kw = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        bt50kw = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        bt100kw = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)
        bt500kw = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt1mw = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt5mw = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        bt10mw = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        bt50mw = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)
        bt100mw = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt500mw = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt1bw = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        btRemainw = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        btCustomw = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)

        bt1kt = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt5kt = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt10kt = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        bt50kt = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        bt100kt = New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)
        bt500kt = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt1mt = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt5mt = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        bt10mt = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        bt50mt = New UIResRectangle(New Point(Get43Ratio() + 402, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)
        bt100mt = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 355), New Size(397, 100), Color.LightPink)
        bt500mt = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 460), New Size(397, 100), Color.LightPink)
        bt1bt = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 565), New Size(397, 100), Color.LightPink)
        btRemaint = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 670), New Size(397, 100), Color.LightPink)
        btCustomt = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), Color.LightPink)

        btWaypointF = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), FranklinColor)
        btWaypointM = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), MichaelColor)
        btWaypointT = New UIResRectangle(New Point(Get43Ratio() + 804, UIMenu.GetSafezoneBounds.Y + 775), New Size(397, 100), TrevorColor)

        selectedButton = Nothing
        hoveredButton = Nothing
        buttonPool = New List(Of UIResRectangle) From {btTrans, btDeposit, btWithdraw, btBalance}
        buttonPool2 = New List(Of UIResRectangle) From {btFranklin, btMichael, btTrevor}
        dPool = New List(Of UIResRectangle) From {bt1kd, bt5kd, bt10kd, bt50kd, bt100kd, bt500kd, bt1md, bt5md, bt10md, bt50md, bt100md, bt500md, bt1bd, btRemaind, btCustomd}
        wPool = New List(Of UIResRectangle) From {bt1kw, bt5kw, bt10kw, bt50kw, bt100kw, bt500kw, bt1mw, bt5mw, bt10mw, bt50mw, bt100mw, bt500mw, bt1bw, btRemainw, btCustomw}
        tPool = New List(Of UIResRectangle) From {bt1kt, bt5kt, bt10kt, bt50kt, bt100kt, bt500kt, bt1mt, bt5mt, bt10mt, bt50mt, bt100mt, bt500mt, bt1bt, btRemaint, btCustomt}
        wpPool = New List(Of UIResRectangle) From {btWaypointF, btWaypointM, btWaypointT}
    End Sub

    Private Sub Bank_Tick(sender As Object, e As EventArgs) Handles Me.Tick
        'UI.ShowSubtitle($"Page ID: {GetActiveWebsiteID()} Web ID: {GetCurrentWebsiteID()} IsFleeca: {IsBrowsingWebsite("www_fleeca_com")}")

        DrawBankBalanceOnHud()

        If _drawBalance OrElse _drawMenu OrElse _drawMsgbox OrElse _drawTransfer OrElse _drawUI OrElse _drawDeposit OrElse _drawWithdraw OrElse _drawTransfer2 Then
            If UI.IsHudComponentActive(HudComponent.CashChange) Then
                DrawCashOnHud()
            End If
        End If

        If _drawUI Then
            DrawUI()

            If ticker >= 30 Then
                password = password & "* "
                ticker = 0
            End If

            If password = "* * * * * " Then
                _drawUI = False
                password = ""
                ticker = 0
                _drawMenu = True
            End If
        End If

        If _drawMenu Then
            DrawMainMenu()
        End If

        If _drawBalance Then
            DrawBalanceMenu()
        End If

        If _drawMsgbox Then
            DrawMessageBox(_msgBoxText)
        End If

        If _drawTransfer Then
            DrawTransferMenu()
        End If

        If _drawDeposit Then
            DrawDepositMenu()
        End If

        If _drawWithdraw Then
            DrawWithdrawMenu()
        End If

        If _drawTransfer2 Then
            DrawTransferMenu2()
        End If

        For Each atm As Prop In World.GetNearbyProps(Game.Player.Character.Position, 4.0F)
            If atmModel.Contains(atm.Model) Then
                Game.DisableControlThisFrame(0, Control.Context)
                If Game.IsControlJustReleased(0, Control.Context) AndAlso Not _drawUI Then
                    Game.Player.Character.Task.SlideTo(atm.Position, atm.Heading)
                    playerCam = World.CreateCamera(GameplayCamera.Position, GameplayCamera.Rotation, GameplayCamera.FieldOfView)
                    World.RenderingCamera = playerCam
                    atmCam = World.CreateCamera(New Vector3(atm.Position.X, atm.Position.Y, atm.Position.Z + 1.15F) - atm.ForwardVector, atm.Rotation, GameplayCamera.FieldOfView)
                    playerCam.InterpTo(atmCam, 1000, True, True)
                    World.RenderingCamera = atmCam
                    Script.Wait(1000)
                    _drawUI = True
                    hoveredButton = btTrans
                End If
            End If
        Next

        If Game.IsControlJustReleased(0, Control.FrontendPauseAlternate) OrElse Game.IsControlJustReleased(0, Control.PhoneCancel) Then
            If _drawMenu Then
                _drawMenu = False
                Game.Player.Character.Task.ClearAll()
                atmCam.InterpTo(playerCam, 1000, True, True)
                World.RenderingCamera = playerCam
                Script.Wait(1000)
                World.RenderingCamera = Nothing
                World.DestroyAllCameras()
            End If
            If _drawBalance Then
                _drawBalance = False
                selectedButton = Nothing
                _drawMenu = True
            End If
            If _drawMsgbox Then
                _drawMsgbox = False
                selectedButton = Nothing
                _drawMenu = True
                hoveredButton = btTrans
            End If
            If _drawTransfer Then
                _drawTransfer = False
                selectedButton = Nothing
                _drawMenu = True
                hoveredButton = btTrans
            End If
            If _drawDeposit Then
                _drawDeposit = False
                selectedButton = Nothing
                _drawMenu = True
                hoveredButton = btDeposit
            End If
            If _drawWithdraw Then
                _drawWithdraw = False
                selectedButton = Nothing
                _drawMenu = True
                hoveredButton = btWithdraw
            End If
            If _drawTransfer2 Then
                _drawTransfer2 = False
                selectedButton = Nothing
                _drawTransfer = True
                hoveredButton = btFranklin
            End If
        End If

        If Game.Player.IsBusted OrElse Game.Player.IsDead Then
            Game.Player.Money = 0
        End If

        AccrueInterest()

        If IsBrowsingWebsite("www_fleeca_com", 2) Then
            ControlEventsWaypoint()
            hoveredButton = btWaypointF
            ShowCursor()
            Dim bgRect As New UIResRectangle(New Point(0, 138), UIMenu.GetScreenResolutionMaintainRatio.ToSize, Color.FromArgb(232, 232, 232)) : bgRect.Draw()
            Dim topRect As New UIResRectangle(New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), Color.FromArgb(38, 143, 68)) : topRect.Draw()
            Dim topSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), 0F, Color.FromArgb(127, Color.FromArgb(38, 143, 68))) : topSpr.Draw()

            Dim grnRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.FromArgb(38, 143, 68)) : grnRect.Draw()
            Dim grnSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.FromArgb(38, 143, 68))) : grnSpr.Draw()
            Dim balance As Long = 0
            Select Case Game.Player.Character.Name
                Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                    balance = fcBank
                Case Game.GetGXTEntry("ACCNA_MIKE")
                    balance = mtBank
                Case Game.GetGXTEntry("ACCNA_TREVOR")
                    balance = tpBank
                Case Else
                    balance = 0
            End Select
            Dim lbIdNo As New UIResText($"{Game.GetGXTEntry("MPATM_ACBA")}:~n~${balance.ToString("###,###")}", New Point(grnRect.Position.X + (grnRect.Size.Width / 2), grnRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbIdNo.Draw()

            btWaypointF.Draw()
            Dim lbWaypoint As New UIResText(Game.GetGXTEntry("HTX_WAYP"), New Point(btWaypointF.Position.X + (btWaypointF.Size.Width / 2), btWaypointF.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbWaypoint.Draw()
        End If

        If IsBrowsingWebsite("www_maze_d_bank_com", 2) Then
            ControlEventsWaypoint()
            hoveredButton = btWaypointM
            ShowCursor()
            Dim bgRect As New UIResRectangle(New Point(0, 138), UIMenu.GetScreenResolutionMaintainRatio.ToSize, Color.FromArgb(231, 232, 231)) : bgRect.Draw()
            Dim topRect As New UIResRectangle(New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), Color.Crimson) : topRect.Draw()
            Dim topSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), 0F, Color.FromArgb(127, Color.Crimson)) : topSpr.Draw()

            Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.Crimson) : redRect.Draw()
            Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
            Dim balance As Long = 0
            Select Case Game.Player.Character.Name
                Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                    balance = fcBank
                Case Game.GetGXTEntry("ACCNA_MIKE")
                    balance = mtBank
                Case Game.GetGXTEntry("ACCNA_TREVOR")
                    balance = tpBank
                Case Else
                    balance = 0
            End Select
            Dim lbIdNo As New UIResText($"{Game.GetGXTEntry("MPATM_ACBA")}:~n~${balance.ToString("###,###")}", New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbIdNo.Draw()

            btWaypointM.Draw()
            Dim lbWaypoint As New UIResText(Game.GetGXTEntry("HTX_WAYP"), New Point(btWaypointM.Position.X + (btWaypointM.Size.Width / 2), btWaypointM.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbWaypoint.Draw()
        End If

        If IsBrowsingWebsite("www_thebankofliberty_com", 2) Then
            ControlEventsWaypoint()
            hoveredButton = btWaypointT
            ShowCursor()
            Dim bgRect As New UIResRectangle(New Point(0, 138), UIMenu.GetScreenResolutionMaintainRatio.ToSize, Color.FromArgb(231, 232, 231)) : bgRect.Draw()
            Dim topRect As New UIResRectangle(New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), Color.RoyalBlue) : topRect.Draw()
            Dim topSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, 138), New Size(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width, 20), 0F, Color.FromArgb(127, Color.RoyalBlue)) : topSpr.Draw()

            Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.RoyalBlue) : redRect.Draw()
            Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.RoyalBlue)) : redSpr.Draw()
            Dim balance As Long = 0
            Select Case Game.Player.Character.Name
                Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                    balance = fcBank
                Case Game.GetGXTEntry("ACCNA_MIKE")
                    balance = mtBank
                Case Game.GetGXTEntry("ACCNA_TREVOR")
                    balance = tpBank
                Case Else
                    balance = 0
            End Select
            Dim lbIdNo As New UIResText($"{Game.GetGXTEntry("MPATM_ACBA")}:~n~${balance.ToString("###,###")}", New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbIdNo.Draw()

            btWaypointT.Draw()
            Dim lbWaypoint As New UIResText(Game.GetGXTEntry("HTX_WAYP"), New Point(btWaypointT.Position.X + (btWaypointT.Size.Width / 2), btWaypointT.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbWaypoint.Draw()
        End If
    End Sub

    Private Sub DrawUI()
        DrawBackground()
        DrawHeader()
        DrawLoginScreen()
        DisableControls()
        Game.Player.Character.Task.ClearAll()
    End Sub

    Private Sub DrawBackground()
        Dim bgColor As Color = Color.FromArgb(231, 232, 231)
        Dim bgRect As New UIResRectangle(New Point(0, 0), UIMenu.GetScreenResolutionMaintainRatio.ToSize, bgColor) : bgRect.Draw()
    End Sub

    Private Sub DrawHeader()
        Dim mzLogo As New Sprite("emailads_maze_bank", "emailads_maze_bank", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y), New Size(384, 96), 0F, Color.White) : mzLogo.Draw()
        Dim redRect As New UIResRectangle(New Point(0, UIMenu.GetSafezoneBounds.Y + 130), New Size(CInt(UIMenu.GetScreenResolutionMaintainRatio.Width), 80), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(0, UIMenu.GetSafezoneBounds.Y + 130), New Size(CInt(UIMenu.GetScreenResolutionMaintainRatio.Width), 80), 180.0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim blkRect As New UIResRectangle(New Point(0, UIMenu.GetSafezoneBounds.Y + 210), New Size(CInt(UIMenu.GetScreenResolutionMaintainRatio.Width), 5), Color.Black) : blkRect.Draw()
    End Sub

    Private Sub DrawLoginScreen()
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbIdNo As New UIResText(Game.GetGXTEntry("collision_sfpfxw"), New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbIdNo.Draw()
        Dim whtRect As New UIResRectangle(New Point(redRect.Position.X + 400, redRect.Position.Y + 200), New Size(400, 150), Color.White) : whtRect.Draw()
        Dim lblPwd As New UIResText(password, New Point(whtRect.Position.X + (whtRect.Size.Width / 2), whtRect.Position.Y + (whtRect.Size.Height / 4)), 2.0F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lblPwd.Draw()
        ticker += 1
    End Sub

    Private Sub DrawMainMenu()
        DrawBackground()
        DrawHeader()
        DrawMain()
        DrawMainMenuInstructionalButton()
        DisableControls()
        ControlEventsMain()
        ButtonEvents()
    End Sub

    Private Sub DrawMain()
        ShowCursor()
        ChangeCursorPointer(5)
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
        Dim lbServ As New UIResText(Game.GetGXTEntry("MPATM_SER"), New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

        btTrans.Draw()
        Dim lbTrans As New UIResText(Game.GetGXTEntry("HUD_IMPORT").UppercaseFirstLetter, New Point(btTrans.Position.X + (btTrans.Size.Width / 2), btTrans.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbTrans.Draw()
        btDeposit.Draw()
        Dim lbDeposit As New UIResText(Game.GetGXTEntry("MPATM_DIDM"), New Point(btDeposit.Position.X + (btDeposit.Size.Width / 2), btDeposit.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbDeposit.Draw()
        btWithdraw.Draw()
        Dim lbWithdraw As New UIResText(Game.GetGXTEntry("MPATM_WITM"), New Point(btWithdraw.Position.X + (btWithdraw.Size.Width / 2), btWithdraw.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbWithdraw.Draw()
        btBalance.Draw()
        Dim lbBalance As New UIResText(Game.GetGXTEntry("W_BA_BAL"), New Point(btBalance.Position.X + (btBalance.Size.Width / 2), btBalance.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbBalance.Draw()
    End Sub

    Private Sub DrawTransferMenu()
        DrawBackground()
        DrawHeader()
        DrawTransfer()
        DrawMainMenuInstructionalButton()
        DisableControls()
        ControlEventsTransfer()
        ButtonEvents()
    End Sub

    Private Sub DrawTransfer()
        ShowCursor()
        ChangeCursorPointer(5)
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
        Dim lbServ As New UIResText(Game.GetGXTEntry("collision_2pa6mh").ToLower.UppercaseFirstLetter, New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

        btMichael.Draw()
        Dim lbMike As New UIResText(Game.GetGXTEntry("ACCNA_MIKE"), New Point(btMichael.Position.X + (btMichael.Size.Width / 2), btMichael.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbMike.Draw()
        btFranklin.Draw()
        Dim lbFrank As New UIResText(Game.GetGXTEntry("ACCNA_FRANKLIN"), New Point(btFranklin.Position.X + (btFranklin.Size.Width / 2), btFranklin.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbFrank.Draw()
        btTrevor.Draw()
        Dim lbTrev As New UIResText(Game.GetGXTEntry("ACCNA_TREVOR"), New Point(btTrevor.Position.X + (btTrevor.Size.Width / 2), btTrevor.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbTrev.Draw()
    End Sub

    Private Sub DrawBalanceMenu()
        ShowCursor()
        DrawBackground()
        DrawHeader()

        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim balance As Long = 0
        Select Case Game.Player.Character.Name
            Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                balance = fcBank
            Case Game.GetGXTEntry("ACCNA_MIKE")
                balance = mtBank
            Case Game.GetGXTEntry("ACCNA_TREVOR")
                balance = tpBank
            Case Else
                balance = 0
        End Select
        Dim bal As String = balance.ToString("###,###")
        If balance = 0 Then bal = "0"
        Dim lbIdNo As New UIResText($"{Game.GetGXTEntry("MPATM_ACBA")}:~n~${bal}", New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbIdNo.Draw()

        DisableControls()
        DrawBalanceInstructionalButton()
    End Sub

    Private Sub DrawMessageBox(text As String)
        ShowCursor()
        DrawBackground()
        DrawHeader()
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 450), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbIdNo As New UIResText(text, New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 80), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbIdNo.Draw()
        DisableControls()
        DrawBalanceInstructionalButton()
    End Sub

    Private Sub DisableControls()
        Game.DisableControlThisFrame(0, Control.Attack)
        Game.DisableControlThisFrame(0, Control.Aim)
        Game.DisableControlThisFrame(0, Control.Attack2)
        Game.DisableControlThisFrame(0, Control.MoveDown)
        Game.DisableControlThisFrame(0, Control.MoveUp)
        Game.DisableControlThisFrame(0, Control.MoveLeft)
        Game.DisableControlThisFrame(0, Control.MoveRight)
        Game.DisableControlThisFrame(0, Control.Jump)
        Game.DisableControlThisFrame(0, Control.Cover)
        Game.DisableControlThisFrame(0, Control.SpecialAbilityPC)
        Game.DisableControlThisFrame(0, Control.Talk)
        Game.DisableControlThisFrame(0, Control.PhoneUp)
        Game.DisableControlThisFrame(0, Control.NextWeapon)
        Game.DisableControlThisFrame(0, Control.PrevWeapon)
        Game.DisableControlThisFrame(0, Control.SelectWeapon)
        Game.DisableControlThisFrame(0, Control.CharacterWheel)
        Game.DisableControlThisFrame(0, Control.FrontendPause)
        Game.DisableControlThisFrame(0, Control.FrontendPauseAlternate)
        Game.DisableControlThisFrame(0, Control.Context)
    End Sub

    Private Sub DrawMainMenuInstructionalButton()
        _scaleform.Render2D()
        _scaleform.CallFunction("CLEAR_ALL")
        _scaleform.CallFunction("TOGGLE_MOUSE_BUTTONS", 0)
        _scaleform.CallFunction("CREATE_CONTAINER")
        _scaleform.CallFunction("SET_DATA_SLOT", 0, GET_CONTROL_INSTRUCTIONAL_BUTTON(Control.PhoneCancel), Game.GetGXTEntry("HUD_INPUT3"))
        _scaleform.CallFunction("SET_DATA_SLOT", 1, GET_CONTROL_INSTRUCTIONAL_BUTTON(Control.PhoneSelect), Game.GetGXTEntry("HUD_INPUT2"))
        _scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1)
    End Sub

    Private Sub DrawBalanceInstructionalButton()
        _scaleform.Render2D()
        _scaleform.CallFunction("CLEAR_ALL")
        _scaleform.CallFunction("TOGGLE_MOUSE_BUTTONS", 0)
        _scaleform.CallFunction("CREATE_CONTAINER")
        _scaleform.CallFunction("SET_DATA_SLOT", 0, GET_CONTROL_INSTRUCTIONAL_BUTTON(Control.PhoneCancel), Game.GetGXTEntry("HUD_INPUT3"))
        _scaleform.CallFunction("DRAW_INSTRUCTIONAL_BUTTONS", -1)
    End Sub

    Private Sub AccrueInterest()
        If TodayIs() = "Monday" Then
            If World.CurrentDayTime.Hours = 0 AndAlso World.CurrentDayTime.Minutes = 0 AndAlso World.CurrentDayTime.Seconds = 0 Then
                Select Case Game.Player.Character.Name
                    Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                        Dim i = Convert.ToInt64(Math.Round(fcBank * interest))
                        DisplayNotificationThisFrame(Game.GetGXTEntry("collision_uouiv2"), Nothing, $"{Game.GetGXTEntry("collision_x68lyf").ToLower.UppercaseFirstLetter}: ${i.ToString("###,###")}~n~{Game.GetGXTEntry("W_BA_BAL")}: ${(i + fcBank).ToString("###,###")}", Icon.CHAR_BANK_FLEECA, True, IconType.DollarSignIcon)
                    Case Game.GetGXTEntry("ACCNA_MIKE")
                        Dim i = Convert.ToInt64(Math.Round(mtBank * interest))
                        DisplayNotificationThisFrame(Game.GetGXTEntry("collision_uouiv2"), Nothing, $"{Game.GetGXTEntry("collision_x68lyf").ToLower.UppercaseFirstLetter}: ${i.ToString("###,###")}~n~{Game.GetGXTEntry("W_BA_BAL")}: ${(i + mtBank).ToString("###,###")}", Icon.CHAR_BANK_MAZE, True, IconType.DollarSignIcon)
                    Case Game.GetGXTEntry("ACCNA_TREVOR")
                        Dim i = Convert.ToInt64(Math.Round(tpBank * interest))
                        DisplayNotificationThisFrame(Game.GetGXTEntry("collision_uouiv2"), Nothing, $"{Game.GetGXTEntry("collision_x68lyf").ToLower.UppercaseFirstLetter}: ${i.ToString("###,###")}~n~{Game.GetGXTEntry("W_BA_BAL")}: ${(i + tpBank).ToString("###,###")}", Icon.CHAR_BANK_BOL, True, IconType.DollarSignIcon)
                End Select

                Dim int = Convert.ToInt64(Math.Round(fcBank * interest))
                fcBank = int + fcBank
                int = Convert.ToInt64(Math.Round(mtBank * interest))
                mtBank = int + mtBank
                int = Convert.ToInt64(Math.Round(tpBank * interest))
                tpBank = int + tpBank

                Script.Wait(1000)
                UpdateBank()
            End If
        End If
    End Sub

    Private Sub Bank_Aborted(sender As Object, e As EventArgs) Handles Me.Aborted
        UpdateBank()
    End Sub

    Private Sub DrawBankBalanceOnHud()
        Dim balance As Long = 0
        Select Case Game.Player.Character.Name
            Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                balance = fcBank
            Case Game.GetGXTEntry("ACCNA_MIKE")
                balance = mtBank
            Case Game.GetGXTEntry("ACCNA_TREVOR")
                balance = tpBank
            Case Else
                balance = 0
        End Select

        Dim height As Integer = 40
        If UI.IsHudComponentActive(HudComponent.WantedStars) Then height += 40
        If UI.IsHudComponentActive(HudComponent.WeaponIcon) Then UI.HideHudComponentThisFrame(HudComponent.WeaponIcon)
        If UI.IsHudComponentActive(HudComponent.CashChange) Then height += 40
        If UI.IsHudComponentActive(HudComponent.Cash) AndAlso Not UI.IsHudComponentActive(HudComponent.WeaponWheelStats) Then
            Dim text As New UIResText($"${balance}", New Point(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width - UIMenu.GetSafezoneBounds.X, UIMenu.GetSafezoneBounds.Y + height), 0.65F, Color.DarkOliveGreen, GTA.Font.Pricedown, UIResText.Alignment.Right) With {.Outline = True}
            text.Draw()
        End If
    End Sub

    Private Sub DrawCashOnHud()
        Dim cash As New UIResText($"${Game.Player.Money}", New Point(UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width - UIMenu.GetSafezoneBounds.X, UIMenu.GetSafezoneBounds.Y), 0.65F, Color.White, GTA.Font.Pricedown, UIResText.Alignment.Right) With {.Outline = True}
        cash.Draw()
    End Sub

    Private Sub DrawDepositMenu()
        DrawBackground()
        DrawHeader()

        ShowCursor()
        ChangeCursorPointer(5)
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
        Dim lbServ As New UIResText(Game.GetGXTEntry("MPATM_DIDM"), New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

        bt1kd.Draw()
        Dim lb1k As New UIResText("$1,000", New Point(bt1kd.Position.X + (bt1kd.Size.Width / 2), bt1kd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 1000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1k.Draw()
        bt5kd.Draw()
        Dim lb5k As New UIResText("$5,000", New Point(bt5kd.Position.X + (bt5kd.Size.Width / 2), bt5kd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 5000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb5k.Draw()
        bt10kd.Draw()
        Dim lb10k As New UIResText("$10,000", New Point(bt10kd.Position.X + (bt10kd.Size.Width / 2), bt10kd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 10000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb10k.Draw()
        bt50kd.Draw()
        Dim lb50k As New UIResText("$50,000", New Point(bt50kd.Position.X + (bt50kd.Size.Width / 2), bt50kd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 50000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb50k.Draw()
        bt100kd.Draw()
        Dim lb100k As New UIResText("$100,000", New Point(bt100kd.Position.X + (bt100kd.Size.Width / 2), bt100kd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 100000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb100k.Draw()
        bt500kd.Draw()
        Dim lb500k As New UIResText("$500,000", New Point(bt500kd.Position.X + (bt500kd.Size.Width / 2), bt500kd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 500000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb500k.Draw()
        bt1md.Draw()
        Dim lb1m As New UIResText("$1,000,000", New Point(bt1md.Position.X + (bt1md.Size.Width / 2), bt1md.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 1000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1m.Draw()
        bt5md.Draw()
        Dim lb5m As New UIResText("$5,000,000", New Point(bt5md.Position.X + (bt5md.Size.Width / 2), bt5md.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 5000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb5m.Draw()
        bt10md.Draw()
        Dim lb10m As New UIResText("$10,000,000", New Point(bt10md.Position.X + (bt10md.Size.Width / 2), bt10md.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 10000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb10m.Draw()
        bt50md.Draw()
        Dim lb50m As New UIResText("$50,000,000", New Point(bt50md.Position.X + (bt50md.Size.Width / 2), bt50md.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 50000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb50m.Draw()
        bt100md.Draw()
        Dim lb100m As New UIResText("$100,000,000", New Point(bt100md.Position.X + (bt100md.Size.Width / 2), bt100md.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 100000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb100m.Draw()
        bt500md.Draw()
        Dim lb500m As New UIResText("$500,000,000", New Point(bt500md.Position.X + (bt500md.Size.Width / 2), bt500md.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 500000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb500m.Draw()
        bt1bd.Draw()
        Dim lb1b As New UIResText("$1,000,000,000", New Point(bt1bd.Position.X + (bt1bd.Size.Width / 2), bt1bd.Position.Y + 30), 0.5F, If(GetDepositRemainingAmount() >= 1000000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1b.Draw()
        btRemaind.Draw()
        Dim lbRemain As New UIResText($"${GetDepositRemainingAmount.ToString("N0")}", New Point(btRemaind.Position.X + (btRemaind.Size.Width / 2), btRemaind.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbRemain.Draw()
        btCustomd.Draw()
        Dim lbCustom As New UIResText(Set_Amount, New Point(btCustomd.Position.X + (btCustomd.Size.Width / 2), btCustomd.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbCustom.Draw()

        DrawMainMenuInstructionalButton()
        DisableControls()
        ControlEventsDeposit()
        ButtonEvents()
    End Sub

    Private Sub DrawWithdrawMenu()
        DrawBackground()
        DrawHeader()

        ShowCursor()
        ChangeCursorPointer(5)
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
        Dim lbServ As New UIResText(Game.GetGXTEntry("MPATM_WITM"), New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

        bt1kw.Draw()
        Dim lb1k As New UIResText("$1,000", New Point(bt1kw.Position.X + (bt1kw.Size.Width / 2), bt1kw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 1000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1k.Draw()
        bt5kw.Draw()
        Dim lb5k As New UIResText("$5,000", New Point(bt5kw.Position.X + (bt5kw.Size.Width / 2), bt5kw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 5000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb5k.Draw()
        bt10kw.Draw()
        Dim lb10k As New UIResText("$10,000", New Point(bt10kw.Position.X + (bt10kw.Size.Width / 2), bt10kw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 10000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb10k.Draw()
        bt50kw.Draw()
        Dim lb50k As New UIResText("$50,000", New Point(bt50kw.Position.X + (bt50kw.Size.Width / 2), bt50kw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 50000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb50k.Draw()
        bt100kw.Draw()
        Dim lb100k As New UIResText("$100,000", New Point(bt100kw.Position.X + (bt100kw.Size.Width / 2), bt100kw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 100000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb100k.Draw()
        bt500kw.Draw()
        Dim lb500k As New UIResText("$500,000", New Point(bt500kw.Position.X + (bt500kw.Size.Width / 2), bt500kw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 500000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb500k.Draw()
        bt1mw.Draw()
        Dim lb1m As New UIResText("$1,000,000", New Point(bt1mw.Position.X + (bt1mw.Size.Width / 2), bt1mw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 1000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1m.Draw()
        bt5mw.Draw()
        Dim lb5m As New UIResText("$5,000,000", New Point(bt5mw.Position.X + (bt5mw.Size.Width / 2), bt5mw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 5000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb5m.Draw()
        bt10mw.Draw()
        Dim lb10m As New UIResText("$10,000,000", New Point(bt10mw.Position.X + (bt10mw.Size.Width / 2), bt10mw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 10000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb10m.Draw()
        bt50mw.Draw()
        Dim lb50m As New UIResText("$50,000,000", New Point(bt50mw.Position.X + (bt50mw.Size.Width / 2), bt50mw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 50000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb50m.Draw()
        bt100mw.Draw()
        Dim lb100m As New UIResText("$100,000,000", New Point(bt100mw.Position.X + (bt100mw.Size.Width / 2), bt100mw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 100000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb100m.Draw()
        bt500mw.Draw()
        Dim lb500m As New UIResText("$500,000,000", New Point(bt500mw.Position.X + (bt500mw.Size.Width / 2), bt500mw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 500000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb500m.Draw()
        bt1bw.Draw()
        Dim lb1b As New UIResText("$1,000,000,000", New Point(bt1bw.Position.X + (bt1bw.Size.Width / 2), bt1bw.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 1000000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1b.Draw()
        btRemainw.Draw()
        Dim lbRemain As New UIResText($"${GetWidhdrawRemainingAmount.ToString("N0")}", New Point(btRemainw.Position.X + (btRemainw.Size.Width / 2), btRemainw.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbRemain.Draw()
        btCustomw.Draw()
        Dim lbCustom As New UIResText(Set_Amount, New Point(btCustomw.Position.X + (btCustomw.Size.Width / 2), btCustomw.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbCustom.Draw()

        DrawMainMenuInstructionalButton()
        DisableControls()
        ControlEventsWithdraw()
        ButtonEvents()
    End Sub

    Private Sub DrawTransferMenu2()
        DrawBackground()
        DrawHeader()

        ShowCursor()
        ChangeCursorPointer(5)
        Dim redRect As New UIResRectangle(New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), Color.Crimson) : redRect.Draw()
        Dim redSpr As New Sprite("commonmenu", "gradient_bgd", New Point(Get43Ratio, UIMenu.GetSafezoneBounds.Y + 260), New Size(1200, 90), 0F, Color.FromArgb(127, Color.Crimson)) : redSpr.Draw()
        Dim lbName As New UIResText($"{Game.Player.Character.Name}", New Point(redRect.Position.X + 5, redRect.Position.Y + 5), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Left) : lbName.Draw()
        Dim lbServ As New UIResText(Game.GetGXTEntry("HUD_IMPORT").UppercaseFirstLetter, New Point(redRect.Position.X + (redRect.Size.Width / 2), redRect.Position.Y + 45), 0.5F, Color.White, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbServ.Draw()

        bt1kt.Draw()
        Dim lb1k As New UIResText("$1,000", New Point(bt1kt.Position.X + (bt1kt.Size.Width / 2), bt1kt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 1000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1k.Draw()
        bt5kt.Draw()
        Dim lb5k As New UIResText("$5,000", New Point(bt5kt.Position.X + (bt5kt.Size.Width / 2), bt5kt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 5000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb5k.Draw()
        bt10kt.Draw()
        Dim lb10k As New UIResText("$10,000", New Point(bt10kt.Position.X + (bt10kt.Size.Width / 2), bt10kt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 10000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb10k.Draw()
        bt50kt.Draw()
        Dim lb50k As New UIResText("$50,000", New Point(bt50kt.Position.X + (bt50kt.Size.Width / 2), bt50kt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 50000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb50k.Draw()
        bt100kt.Draw()
        Dim lb100k As New UIResText("$100,000", New Point(bt100kt.Position.X + (bt100kt.Size.Width / 2), bt100kt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 100000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb100k.Draw()
        bt500kt.Draw()
        Dim lb500k As New UIResText("$500,000", New Point(bt500kt.Position.X + (bt500kt.Size.Width / 2), bt500kt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 500000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb500k.Draw()
        bt1mt.Draw()
        Dim lb1m As New UIResText("$1,000,000", New Point(bt1mt.Position.X + (bt1mt.Size.Width / 2), bt1mt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 1000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1m.Draw()
        bt5mt.Draw()
        Dim lb5m As New UIResText("$5,000,000", New Point(bt5mt.Position.X + (bt5mt.Size.Width / 2), bt5mt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 5000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb5m.Draw()
        bt10mt.Draw()
        Dim lb10m As New UIResText("$10,000,000", New Point(bt10mt.Position.X + (bt10mt.Size.Width / 2), bt10mt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 10000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb10m.Draw()
        bt50mt.Draw()
        Dim lb50m As New UIResText("$50,000,000", New Point(bt50mt.Position.X + (bt50mt.Size.Width / 2), bt50mt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 50000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb50m.Draw()
        bt100mt.Draw()
        Dim lb100m As New UIResText("$100,000,000", New Point(bt100mt.Position.X + (bt100mt.Size.Width / 2), bt100mt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 100000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb100m.Draw()
        bt500mt.Draw()
        Dim lb500m As New UIResText("$500,000,000", New Point(bt500mt.Position.X + (bt500mt.Size.Width / 2), bt500mt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 500000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb500m.Draw()
        bt1bt.Draw()
        Dim lb1b As New UIResText("$1,000,000,000", New Point(bt1bt.Position.X + (bt1bt.Size.Width / 2), bt1bt.Position.Y + 30), 0.5F, If(GetWidhdrawRemainingAmount() >= 1000000000, Color.Black, Color.Gray), GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lb1b.Draw()
        btRemaint.Draw()
        Dim lbRemain As New UIResText($"${GetWidhdrawRemainingAmount.ToString("N0")}", New Point(btRemaint.Position.X + (btRemaint.Size.Width / 2), btRemaint.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbRemain.Draw()
        btCustomt.Draw()
        Dim lbCustom As New UIResText(Set_Amount, New Point(btCustomt.Position.X + (btCustomt.Size.Width / 2), btCustomt.Position.Y + 30), 0.5F, Color.Black, GTA.Font.ChaletLondon, UIResText.Alignment.Centered) : lbCustom.Draw()

        DrawMainMenuInstructionalButton()
        DisableControls()
        ControlEventsTransfer2()
        ButtonEvents()
    End Sub

End Class
