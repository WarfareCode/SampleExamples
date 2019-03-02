function varargout = ObjectModel(varargin)
% OBJECTMODEL MATLAB code for ObjectModel.fig
%      OBJECTMODEL, by itself, creates a new OBJECTMODEL or raises the existing
%      singleton*.
%
%      H = OBJECTMODEL returns the handle to a new OBJECTMODEL or the handle to
%      the existing singleton*.
%
%      OBJECTMODEL('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in OBJECTMODEL.M with the given input arguments.
%
%      OBJECTMODEL('Property','Value',...) creates a new OBJECTMODEL or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before ObjectModel_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to ObjectModel_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help ObjectModel

% Last Modified by GUIDE v2.5 03-Oct-2012 17:18:28

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @ObjectModel_OpeningFcn, ...
                   'gui_OutputFcn',  @ObjectModel_OutputFcn, ...
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


% --- Executes just before ObjectModel is made visible.
function ObjectModel_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to ObjectModel (see VARARGIN)

% Choose default command line output for ObjectModel
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% Create a 3d globe control
global voControl
voControl = actxcontrol('STKX11.VOControl', [50 100 275 275]);

% Create a 2d map control
global mapControl
mapControl = actxcontrol('STKX11.2DControl', [350 100 275 275]);

% obtain a handle to the application
global OM
OM = actxserver('AgStkObjects11.AgStkObjectRoot')

% UIWAIT makes ObjectModel wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = ObjectModel_OutputFcn(hObject, eventdata, handles) 
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

global OM

OM.UnitPreferences.SetCurrentUnit('LatitudeUnit', 'deg')
OM.UnitPreferences.SetCurrentUnit('LongitudeUnit', 'deg')
OM.UnitPreferences.SetCurrentUnit('DateFormat', 'UTCG')

OM.CloseScenario
OM.NewScenario('Test')
sc = OM.CurrentScenario
sc.StartTime = '1 Jul 1999 00:00:00.000'
sc.StopTime = '6 Jul 1999 00:00:00.000'
sc.Epoch = '3 Jul 1999 00:00:00.000'
gv = sc.Children.New(9, 'GV1')
gv.SetRouteType(9)

prop = gv.Route
prop.Method = 2

wpt = prop.Waypoints.Add()
wpt.Time = '1 Jul 1999 08:00:00.000'
wpt.Latitude = 0
wpt.Longitude = 0
wpt = prop.Waypoints.Add()
wpt.Time = '2 Jul 1999 12:00:00.000'
wpt.Latitude = 0
wpt.Longitude = 5
wpt = prop.Waypoints.Add()
wpt.Time = '3 Jul 1999 08:00:00.000'
wpt.Latitude = 5
wpt.Longitude = 5
wpt = prop.Waypoints.Add()
wpt.Time = '4 Jul 1999 08:00:00.000'
wpt.Latitude = 5
wpt.Longitude = 0
wpt = prop.Waypoints.Add()
wpt.Time = '5 Jul 1999 08:00:00.000'
wpt.Latitude = 0
wpt.Longitude = 0
prop.Propagate
gfx = gv.Graphics
attr = gfx.Attributes
attr.Color = 255

disp('check')
get(gv.DataProviders)
adr = gv.DataProviders.invoke('Item', 'Cartesian Position').Group.invoke('Item', 'J2000').Exec('1 Jul 1999 08:00:00.000', '5 Jul 1999 08:00:00.000', 2400)
disp('Data Provider Results (J2000 Cartesian Position): ')
disp('Intervals: ')
disp(adr.Intervals.Count)
disp('DataSets: ')
disp(adr.DataSets.Count)
    
for i = 0:adr.DataSets.Count-1
    dataset = adr.Intervals.invoke('Item', 0).DataSets.invoke('Item', i)
    values = dataset.GetValues
    disp(dataset.ElementName)
    disp(values(1))
end

% --- Executes when user attempts to close figure1.
function figure1_CloseRequestFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

global OM
OM.CloseScenario()

% Hint: delete(hObject) closes the figure
delete(hObject);

% --- Executes during object creation, after setting all properties.
function figure1_CreateFcn(hObject, eventdata, handles)
% hObject    handle to figure1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called


