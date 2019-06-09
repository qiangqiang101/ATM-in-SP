Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions
Imports GTA
Imports GTA.Math
Imports GTA.Native
Imports INMNativeUI

Public Module Helper

    Public btTrans, btDeposit, btWithdraw, btBalance, selectedButton, btFranklin, btMichael, btTrevor, hoveredButton, btWaypointF, btWaypointM, btWaypointT As UIResRectangle
    Public bt1kd, bt5kd, bt10kd, bt50kd, bt100kd, bt500kd, bt1md, bt5md, bt10md, bt50md, bt100md, bt500md, bt1bd, btRemaind, btCustomd As UIResRectangle
    Public bt1kw, bt5kw, bt10kw, bt50kw, bt100kw, bt500kw, bt1mw, bt5mw, bt10mw, bt50mw, bt100mw, bt500mw, bt1bw, btRemainw, btCustomw As UIResRectangle
    Public bt1kt, bt5kt, bt10kt, bt50kt, bt100kt, bt500kt, bt1mt, bt5mt, bt10mt, bt50mt, bt100mt, bt500mt, bt1bt, btRemaint, btCustomt As UIResRectangle
    Public buttonPool, buttonPool2, dPool, wPool, tPool, wpPool As List(Of UIResRectangle)
    Public selectedTrans As PlayerType = PlayerType.None
    Public FranklinColor As Color = Color.LightGreen, TrevorColor As Color = Color.LightBlue, MichaelColor As Color = Color.LightPink
    Public FranklinColorH As Color = Color.FromArgb(38, 143, 68), TrevorColorH As Color = Color.RoyalBlue, MichaelColorH As Color = Color.Crimson

    Public _drawUI As Boolean = False, _drawMenu As Boolean = False, _drawBalance As Boolean = False, _drawMsgbox As Boolean = False, _drawTransfer As Boolean = False
    Public _drawTransfer2 As Boolean = False, _drawDeposit As Boolean = False, _drawWithdraw As Boolean = False
    Public fcBank, mtBank, tpBank As Long, transFee, withFee, depFee As Integer, lostCashBusted, lostCashDead As Boolean, interest As Single
    Public _msgBoxText As String = Nothing
    Public atmLocations As New List(Of Vector3) From {
        New Vector3(-1109.797F, -1690.808F, 4.375014F),
        New Vector3(-821.6062F, -1081.885F, 11.13243F),
        New Vector3(-537.8409F, -854.5145F, 29.28953F),
        New Vector3(-1315.744F, -834.6907F, 16.96173F),
        New Vector3(-1314.786F, -835.9669F, 16.96015F),
        New Vector3(-1570.069F, -546.6727F, 34.95547F),
        New Vector3(-1571.018F, -547.3666F, 34.95734F),
        New Vector3(-866.6416F, -187.8008F, 37.84286F),
        New Vector3(-867.6165F, -186.1373F, 37.8433F),
        New Vector3(-721.1284F, -415.5296F, 34.98175F),
        New Vector3(-254.3758F, -692.4947F, 33.63751F),
        New Vector3(24.37422F, -946.0142F, 29.35756F),
        New Vector3(130.1186F, -1292.669F, 29.26953F),
        New Vector3(129.7023F, -1291.954F, 29.26953F),
        New Vector3(129.2096F, -1291.14F, 29.26953F),
        New Vector3(288.8256F, -1282.364F, 29.64128F),
        New Vector3(1077.768F, -776.4548F, 58.23997F),
        New Vector3(527.2687F, -160.7156F, 57.08937F),
        New Vector3(-867.5897F, -186.1757F, 37.84291F),
        New Vector3(-866.6556F, -187.7766F, 37.84278F),
        New Vector3(-1205.024F, -326.2916F, 37.83985F),
        New Vector3(-1205.703F, -324.7474F, 37.85942F),
        New Vector3(-1570.167F, -546.7214F, 34.95663F),
        New Vector3(-1571.056F, -547.3947F, 34.95724F),
        New Vector3(-57.64693F, -92.66162F, 57.77995F),
        New Vector3(527.3583F, -160.6381F, 57.0933F),
        New Vector3(-165.1658F, 234.8314F, 94.92194F),
        New Vector3(-165.1503F, 232.7887F, 94.92194F),
        New Vector3(-2072.445F, -317.3048F, 13.31597F),
        New Vector3(-3241.082F, 997.5428F, 12.55044F),
        New Vector3(-1091.462F, 2708.637F, 18.95291F),
        New Vector3(1172.492F, 2702.492F, 38.17477F),
        New Vector3(1171.537F, 2702.492F, 38.17542F),
        New Vector3(1822.637F, 3683.131F, 34.27678F),
        New Vector3(1686.753F, 4815.806F, 42.00874F),
        New Vector3(1701.209F, 6426.569F, 32.76408F),
        New Vector3(-95.54314F, 6457.19F, 31.46093F),
        New Vector3(-97.23336F, 6455.469F, 31.46682F),
        New Vector3(-386.7451F, 6046.102F, 31.50172F),
        New Vector3(-1091.42F, 2708.629F, 18.95568F),
        New Vector3(5.132F, -919.7711F, 29.55953F),
        New Vector3(-660.703F, -853.971F, 24.484F),
        New Vector3(-2293.827F, 354.817F, 174.602F),
        New Vector3(-2294.637F, 356.553F, 174.602F),
        New Vector3(-2295.377F, 358.241F, 174.648F),
        New Vector3(-1409.782F, -100.41F, 52.387F),
        New Vector3(-1410.279F, -98.649F, 52.436F)}

    Public config As ScriptSettings = ScriptSettings.Load("scripts\ATM.ini")

    'Localization
    Public Set_Amount As String = "NULL"

    Public Function ScreenResolution() As Size
        Return New Size(Game.ScreenResolution.Width + UIMenu.GetSafezoneBounds.X, Game.ScreenResolution.Height + UIMenu.GetSafezoneBounds.Y)
    End Function

    <Extension()>
    Public Function ToSize(sizef As SizeF) As Size
        Return New Size(CInt(sizef.Width), CInt(sizef.Height))
    End Function

    Public Function Get43Ratio() As Integer
        Dim oriWidth As Integer = UIMenu.GetScreenResolutionMaintainRatio.ToSize.Width
        Dim a As Integer = oriWidth / 16 '120
        Dim b As Integer = a * 10 '1200
        Dim c As Integer = oriWidth - b '720
        Return System.Math.Round(c / 2)
    End Function

    <Extension()>
    Public Function Name(ped As Ped) As String
        Select Case ped.Model
            Case PedHash.Franklin
                Return Game.GetGXTEntry("ACCNA_FRANKLIN") '"Franklin Clinton"
            Case PedHash.Michael
                Return Game.GetGXTEntry("ACCNA_MIKE") '"Michael De Santa"
            Case PedHash.Trevor
                Return Game.GetGXTEntry("ACCNA_TREVOR") '"Trevor Philips"
            Case Else
                Return Game.Player.Name
        End Select
    End Function

    Public Sub ShowCursor()
        Native.Function.Call(Hash._SHOW_CURSOR_THIS_FRAME)
    End Sub

    Public Sub ChangeCursorPointer(cursor As CursorType)
        Native.Function.Call(Hash._0x8DB8CFFD58B62552, cursor)
    End Sub

    Public Enum CursorType
        None
        Normal
        TransparentNormal
        PreGrab
        Grab
        Fuck
        LeftArrow
        RightArrow
        UpArrow
        DownArrow
        HorizontalExpand
        Add
        Remove
    End Enum

    Public Function GET_CONTROL_INSTRUCTIONAL_BUTTON(control As GTA.Control) As String
        Return Native.Function.Call(Of String)(Hash._0x0499D7B09FC9B407, 2, control, 0)
    End Function

    <Extension()>
    Public Function IsNumberic(number As String) As Boolean
        Return Regex.IsMatch(number, "^[0-9 ]+$")
    End Function

    <Extension()>
    Public Function IsBusted(player As Player) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash.IS_PLAYER_BEING_ARRESTED, player, 0)
    End Function

    Public Function GetPlayerIndex() As Integer
        Dim player As Player = Game.Player

        Select Case CType(player.Character.Model.Hash, PedHash)
            Case PedHash.Michael
                Return 0
            Case PedHash.Franklin
                Return 1
            Case PedHash.Trevor
                Return 2
            Case Else
                Return 3
        End Select
    End Function

    Public Sub DisplayHelpTextThisFrame(ByVal [text] As String)
        Native.Function.Call(Hash._0x8509B634FBE7DA11, "STRING")
        Native.Function.Call(Hash._0x6C188BE134E074AA, [text])
        Native.Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1)
    End Sub

    Public Function TodayIs() As String
        Return [Enum].GetName(GetType(DayOfWeek), Native.Function.Call(Of Integer)(Hash.GET_CLOCK_DAY_OF_WEEK))
    End Function

    Public Sub DisplayNotificationThisFrame(Sender As String, Subject As String, Message As String, Icon As Icon, Flash As Boolean, Type As IconType)
        Native.Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING")
        Native.Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, Message)
        Native.Function.Call(Hash._SET_NOTIFICATION_MESSAGE, Icon.ToString("F"), Icon.ToString("F"), Flash, Type, Sender, Subject)
        Native.Function.Call(Hash._DRAW_NOTIFICATION, False, True)
    End Sub

    Public Enum IconType
        ChatBox = 1
        Email = 2
        AddFriendRequest = 3
        Nothing4 = 4
        Nothing5 = 5
        Nothing6 = 6
        RightJumpingArrow = 7
        RPIcon = 8
        DollarSignIcon = 9
    End Enum

    Public Enum Icon
        CHAR_ABIGAIL
        CHAR_ALL_PLAYERS_CONF
        CHAR_AMANDA
        CHAR_AMMUNATION
        CHAR_ANDREAS
        CHAR_ANTONIA
        CHAR_ARTHUR
        CHAR_ASHLEY
        CHAR_BANK_BOL
        CHAR_BANK_FLEECA
        CHAR_BANK_MAZE
        CHAR_BARRY
        CHAR_BEVERLY
        CHAR_BIKESITE
        CHAR_BLANK_ENTRY
        CHAR_BLIMP
        CHAR_BLOCKED
        CHAR_BOATSITE
        CHAR_BROKEN_DOWN_GIRL
        CHAR_BUGSTARS
        CHAR_CALL911
        CHAR_CARSITE
        CHAR_CARSITE2
        CHAR_CASTRO
        CHAR_CHAT_CALL
        CHAR_CHEF
        CHAR_CHENG
        CHAR_CHENGSR
        CHAR_CHOP
        CHAR_CREATOR_PORTRAITS
        CHAR_CRIS
        CHAR_DAVE
        CHAR_DEFAULT
        CHAR_DENISE
        CHAR_DETONATEBOMB
        CHAR_DETONATEPHONE
        CHAR_DEVIN
        CHAR_DIAL_A_SUB
        CHAR_DOM
        CHAR_DOMESTIC_GIRL
        CHAR_DREYFUSS
        CHAR_DR_FRIEDLANDER
        CHAR_EPSILON
        CHAR_ESTATE_AGENT
        CHAR_FACEBOOK
        CHAR_FILMNOIR
        CHAR_FLOYD
        CHAR_FRANKLIN
        CHAR_FRANK_TREV_CONF
        CHAR_GAYMILITARY
        CHAR_HAO
        CHAR_HITCHER_GIRL
        CHAR_HUMANDEFAULT
        CHAR_HUNTER
        CHAR_JIMMY
        CHAR_JIMMY_BOSTON
        CHAR_JOE
        CHAR_JOSEF
        CHAR_JOSH
        CHAR_LAMAR
        CHAR_LAZLOW
        CHAR_LESTER
        CHAR_LESTER_DEATHWISH
        CHAR_LEST_FRANK_CONF
        CHAR_LEST_MIKE_CONF
        CHAR_LIFEINVADER
        CHAR_LS_CUSTOMS
        CHAR_LS_TOURIST_BOARD
        CHAR_MANUEL
        CHAR_MARNIE
        CHAR_MARTIN
        CHAR_MARY_ANN
        CHAR_MAUDE
        CHAR_MECHANIC
        CHAR_MICHAEL
        CHAR_MIKE_FRANK_CONF
        CHAR_MIKE_TREV_CONF
        CHAR_MILSITE
        CHAR_MINOTAUR
        CHAR_MOLLY
        CHAR_MP_ARMY_CONTACT
        CHAR_MP_BIKER_BOSS
        CHAR_MP_BIKER_MECHANIC
        CHAR_MP_BRUCIE
        CHAR_MP_DETONATEPHONE
        CHAR_MP_FAM_BOSS
        CHAR_MP_FIB_CONTACT
        CHAR_MP_FM_CONTACT
        CHAR_MP_GERALD
        CHAR_MP_JULIO
        CHAR_MP_MECHANIC
        CHAR_MP_MERRYWEATHER
        CHAR_MP_MEX_BOSS
        CHAR_MP_MEX_DOCKS
        CHAR_MP_MEX_LT
        CHAR_MP_MORS_MUTUAL
        CHAR_MP_PROF_BOSS
        CHAR_MP_RAY_LAVOY
        CHAR_MP_ROBERTO
        CHAR_MP_SNITCH
        CHAR_MP_STRETCH
        CHAR_MP_STRIPCLUB_PR
        CHAR_MRS_THORNHILL
        CHAR_MULTIPLAYER
        CHAR_NIGEL
        CHAR_OMEGA
        CHAR_ONEIL
        CHAR_ORTEGA
        CHAR_OSCAR
        CHAR_PATRICIA
        CHAR_PEGASUS_DELIVERY
        CHAR_PLANESITE
        CHAR_PROPERTY_ARMS_TRAFFICKING
        CHAR_PROPERTY_BAR_AIRPORT
        CHAR_PROPERTY_BAR_BAYVIEW
        CHAR_PROPERTY_BAR_CAFE_ROJO
        CHAR_PROPERTY_BAR_COCKOTOOS
        CHAR_PROPERTY_BAR_ECLIPSE
        CHAR_PROPERTY_BAR_FES
        CHAR_PROPERTY_BAR_HEN_HOUSE
        CHAR_PROPERTY_BAR_HI_MEN
        CHAR_PROPERTY_BAR_HOOKIES
        CHAR_PROPERTY_BAR_IRISH
        CHAR_PROPERTY_BAR_LES_BIANCO
        CHAR_PROPERTY_BAR_MIRROR_PARK
        CHAR_PROPERTY_BAR_PITCHERS
        CHAR_PROPERTY_BAR_SINGLETONS
        CHAR_PROPERTY_BAR_TEQUILALA
        CHAR_PROPERTY_BAR_UNBRANDED
        CHAR_PROPERTY_CAR_MOD_SHOP
        CHAR_PROPERTY_CAR_SCRAP_YARD
        CHAR_PROPERTY_CINEMA_DOWNTOWN
        CHAR_PROPERTY_CINEMA_MORNINGWOOD
        CHAR_PROPERTY_CINEMA_VINEWOOD
        CHAR_PROPERTY_GOLF_CLUB
        CHAR_PROPERTY_PLANE_SCRAP_YARD
        CHAR_PROPERTY_SONAR_COLLECTIONS
        CHAR_PROPERTY_TAXI_LOT
        CHAR_PROPERTY_TOWING_IMPOUND
        CHAR_PROPERTY_WEED_SHOP
        CHAR_RON
        CHAR_SAEEDA
        CHAR_SASQUATCH
        CHAR_SIMEON
        CHAR_SOCIAL_CLUB
        CHAR_SOLOMON
        CHAR_STEVE
        CHAR_STEVE_MIKE_CONF
        CHAR_STEVE_TREV_CONF
        CHAR_STRETCH
        CHAR_STRIPPER_CHASTITY
        CHAR_STRIPPER_CHEETAH
        CHAR_STRIPPER_FUFU
        CHAR_STRIPPER_INFERNUS
        CHAR_STRIPPER_JULIET
        CHAR_STRIPPER_NIKKI
        CHAR_STRIPPER_PEACH
        CHAR_STRIPPER_SAPPHIRE
        CHAR_TANISHA
        CHAR_TAXI
        CHAR_TAXI_LIZ
        CHAR_TENNIS_COACH
        CHAR_TOW_TONYA
        CHAR_TRACEY
        CHAR_TREVOR
        CHAR_WADE
        CHAR_YOUTUBE
    End Enum

    <Extension()>
    Public Function UppercaseFirstLetter(ByVal val As String) As String
        If String.IsNullOrEmpty(val) Then Return val
        Dim array() As Char = val.ToCharArray
        array(0) = Char.ToUpper(array(0))
        Return New String(array)
    End Function

    Public Function GetCurrentWebsiteID() As Integer
        Return Native.Function.Call(Of Integer)(Hash.GET_CURRENT_WEBSITE_ID)
    End Function

    Public Function GetActiveWebsiteID() As Integer
        Return Native.Function.Call(Of Integer)(Hash._0x01A358D9128B7A86)
    End Function

    Public Function IsBrowsingWebsite(weburl As String, Optional pageid As Integer = -1) As Boolean
        If pageid = -1 Then
            Return Native.Function.Call(Of Boolean)(Hash._HAS_NAMED_SCALEFORM_MOVIE_LOADED, weburl)
            Exit Function
        Else
            If Native.Function.Call(Of Boolean)(Hash._HAS_NAMED_SCALEFORM_MOVIE_LOADED, weburl) AndAlso GetActiveWebsiteID() = pageid Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function IsScaleformMovieLoaded(scaleform As String) As Boolean
        Return Native.Function.Call(Of Boolean)(Hash._HAS_NAMED_SCALEFORM_MOVIE_LOADED, scaleform)
    End Function

    Public Sub SetCursorLocation(pos As PointF)
        Native.Function.Call(Hash._0xFC695459D4D0E219, pos.X, pos.Y)
    End Sub

    Public Function GetPlayerBankBalance() As Long
        Select Case Game.Player.Character.Name
            Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                Return fcBank
            Case Game.GetGXTEntry("ACCNA_MIKE")
                Return mtBank
            Case Game.GetGXTEntry("ACCNA_TREVOR")
                Return tpBank
        End Select
        Return 0
    End Function

    Public Function GetDepositRemainingAmount() As Integer
        Return Game.Player.Money - depFee - 1
    End Function

    Public Function GetWidhdrawRemainingAmount() As Integer
        Dim result As Integer = 0
        Dim int32max As Long = &H7FFFFFFF
        Dim curBank As Long = GetPlayerBankBalance()
        Dim curMoney As Integer = Game.Player.Money

        If curBank >= int32max Then
            result = int32max - curMoney - withFee - 1
        Else
            result = curBank - withFee - 1
        End If

        Return result
    End Function

    Public Sub DepositMoney(amount As Integer)
        If amount >= Game.Player.Money Then
            _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
            _drawDeposit = False
            _drawMsgbox = True
        Else
            Select Case Game.Player.Character.Name
                Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                    fcBank += amount
                Case Game.GetGXTEntry("ACCNA_MIKE")
                    mtBank += amount
                Case Game.GetGXTEntry("ACCNA_TREVOR")
                    tpBank += amount
            End Select
            Game.Player.Money = (Game.Player.Money - amount - depFee)
            UpdateBank()
            _msgBoxText = Game.GetGXTEntry("MPATM_TRANCOM")
            _drawDeposit = False
            _drawMsgbox = True
        End If
    End Sub

    Public Sub WithdrawMoney(amount As Integer)
        Try
            If amount >= GetPlayerBankBalance() Then
                _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
                _drawWithdraw = False
                _drawMsgbox = True
            Else
                Select Case Game.Player.Character.Name
                    Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                        If amount >= fcBank Then amount = fcBank
                        fcBank -= amount
                    Case Game.GetGXTEntry("ACCNA_MIKE")
                        If amount >= mtBank Then amount = mtBank
                        mtBank -= amount
                    Case Game.GetGXTEntry("ACCNA_TREVOR")
                        If amount >= tpBank Then amount = tpBank
                        tpBank -= amount
                End Select
                Game.Player.Money = (Game.Player.Money + amount - withFee)
                UpdateBank()
                _msgBoxText = Game.GetGXTEntry("MPATM_TRANCOM")
                _drawWithdraw = False
                _drawMsgbox = True
            End If
        Catch ex As Exception
            _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
            _drawWithdraw = False
            _drawMsgbox = True
        End Try
    End Sub

    Public Sub TransferMoney(amount As Long, transferTo As PlayerType)
        If amount >= GetPlayerBankBalance() Then
            _msgBoxText = Game.GetGXTEntry("collision_9f1nje9")
            _drawTransfer2 = False
            _drawMsgbox = True
        Else
            Select Case transferTo
                Case PlayerType.Michael
                    Select Case Game.Player.Character.Name
                        Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                            If amount >= fcBank Then amount = fcBank
                            mtBank += amount
                            fcBank -= (amount + transFee)
                        Case Game.GetGXTEntry("ACCNA_TREVOR")
                            If amount >= tpBank Then amount = tpBank
                            mtBank += amount
                            tpBank -= (amount + transFee)
                    End Select
                Case PlayerType.Franklin
                    Select Case Game.Player.Character.Name
                        Case Game.GetGXTEntry("ACCNA_MIKE")
                            If amount >= mtBank Then amount = mtBank
                            fcBank += amount
                            mtBank -= (amount + transFee)
                        Case Game.GetGXTEntry("ACCNA_TREVOR")
                            If amount >= tpBank Then amount = tpBank
                            fcBank += amount
                            tpBank -= (amount + transFee)
                    End Select
                Case PlayerType.Trevor
                    Select Case Game.Player.Character.Name
                        Case Game.GetGXTEntry("ACCNA_FRANKLIN")
                            If amount >= fcBank Then amount = fcBank
                            tpBank += amount
                            fcBank -= (amount + transFee)
                        Case Game.GetGXTEntry("ACCNA_MIKE")
                            If amount >= mtBank Then amount = mtBank
                            tpBank += amount
                            mtBank -= (amount + transFee)
                    End Select
            End Select
            UpdateBank()
            _msgBoxText = Game.GetGXTEntry("MPATM_TRANCOM")
            _drawTransfer2 = False
            _drawMsgbox = True
        End If
    End Sub

    Public Enum PlayerType
        None = -1
        Michael
        Franklin
        Trevor
        Player3
    End Enum

    Public Function RequestAdditionTextFile(ByVal textname As String, ByVal Optional timeout As Integer = 1000) As Boolean
        If Not Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, textname, 9) Then
            Native.Function.Call(Hash.CLEAR_ADDITIONAL_TEXT, 9, True)
            Native.Function.Call(Hash.REQUEST_ADDITIONAL_TEXT, textname, 9)
            Dim [end] As Integer = Game.GameTime + timeout

            If True Then
                While Game.GameTime < [end]
                    If Native.Function.Call(Of Boolean)(Hash.HAS_THIS_ADDITIONAL_TEXT_LOADED, textname, 9) Then Return True
                    Script.Yield()
                End While
                Return False
            End If
        End If

        Return True
    End Function

    Public Function GetNearestATM() As Vector3
        Return atmLocations.ToArray.OrderBy(Function(x) System.Math.Abs(x.DistanceTo(Game.Player.Character.Position))).First
    End Function

    <Extension>
    Public Sub SetWaypoint(waypoint As Vector3)
        Native.Function.Call(Hash.SET_NEW_WAYPOINT, waypoint.X, waypoint.Y)
    End Sub

End Module