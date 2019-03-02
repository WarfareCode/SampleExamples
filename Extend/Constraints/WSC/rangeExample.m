% NOTE: for this example to work, this m-file must be on your Matlab path
%
% Use SetPath in Matlab to set the path or copy this m-file to your m-file
% working area

function [range] = rangeExample(relX, relY, relZ)

% Compute magnitude of vector

vec = [relX; relY; relZ];

magSqr = vec'*vec;

range = sqrt(magSqr);
