%handles scenario new events in the example program
function stkapp_catch_scenario_new(varargin)

global EXAMPLE_APP

disp('New Scenario created.');
disp(varargin{3});
