function varargout = example(varargin)
% EXAMPLE MATLAB code for example.fig
%      EXAMPLE, by itself, creates a new EXAMPLE or raises the existing
%      singleton*.
%
%      H = EXAMPLE returns the handle to a new EXAMPLE or the handle to
%      the existing singleton*.
%
%      EXAMPLE('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in EXAMPLE.M with the given input arguments.
%
%      EXAMPLE('Property','Value',...) creates a new EXAMPLE or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before example_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to example_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help example

% Last Modified by GUIDE v2.5 27-Sep-2012 17:22:22

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @example_OpeningFcn, ...
                   'gui_OutputFcn',  @example_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before example is made visible.
function example_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to example (see VARARGIN)

% Choose default command line output for example
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

global EXAMPLE_APP;

% Obtain a handle to the application
EXAMPLE_APP = actxserver('STKX11.Application');

%Register the applications callback
EXAMPLE_APP.registerevent({'onScenarioNew','stkapp_catch_scenario_new'});

% Create a 3d globe control
global voControl
voControl = actxcontrol('STKX11.VOControl', [50 75 275 275]);
registerevent(voControl, {'MouseMove', str2func('voControl_MouseMove')});

% Create a 2d map control
global mapControl
mapControl = actxcontrol('STKX11.2DControl', [350 75 275 275]);
registerevent(mapControl, {'DblClick', str2func('mapControl_DblClick')});

% set background color of VO control
% OLE colors are blue, green, red two-byte values. Each value can 
% therefore from 0 to 255
bgcolorRed = 128;
bgcolorGreen = 0;
bgcolorBlue = 0;
bgcolor = (bgcolorBlue * 256^2) + (bgcolorGreen * 256^1) + (bgcolorRed * 256^0); 
set(voControl,'Backcolor',bgcolor);

% Enables the connect command interface
set(EXAMPLE_APP,'EnableConnect',true);
set(EXAMPLE_APP,'ConnectPort',5001);

% UIWAIT makes example wait for user response (see UIRESUME)
% uiwait(handles.figure1);

% --- Outputs from this function are returned to the command line.
function varargout = example_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on button press in pushbutton1.
function pushbutton1_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

global mapControl;
global voControl;
invoke(mapControl.Application, 'ExecuteCommand','New / Scenario Test');


% --- Executes when user attempts to close figure1.
function figure1_CloseRequestFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

global mapControl;
invoke(mapControl.Application, 'ExecuteCommand','Unload / *');

global EXAMPLE_APP;

release(EXAMPLE_APP);
EXAMPLE_APP = 0;

% Hint: delete(hObject) closes the figure
delete(hObject);

% --- Executes on button press in pushbutton2.
function pushbutton2_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

global mapControl;
invoke(mapControl, 'ZoomIn');

% --- Executes on button press in pushbutton3.
function pushbutton3_Callback(hObject, eventdata, handles)
% hObject    handle to pushbutton3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

global mapControl;
invoke(mapControl, 'ZoomOut');

% --------------------------------------------------------------------
function mapControl_DblClick(varargin)
% hObject    handle to activex1 (see GCBO)
% eventdata  structure with parameters passed to COM event listener
% handles    structure with handles and user data (see GUIDATA)
disp('AGI Map Control double-click');


% --------------------------------------------------------------------
function voControl_MouseMove(varargin)
% hObject    handle to activex2 (see GCBO)
% eventdata  structure with parameters passed to COM event listener
% handles    structure with handles and user data (see GUIDATA)

global pickInfo;
global voControl;

pickInfo = voControl.PickInfo(varargin{5},varargin{6});

if pickInfo.isLatLonAltValid()
	labelstring=['3d Globe Control Lat: ',num2str(pickInfo.lat),...
         ' Lon: ',num2str(pickInfo.lon)];
	disp(labelstring);
 end

release(pickInfo);
