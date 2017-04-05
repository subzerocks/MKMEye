/*
	Magic Vision - MKM Edition 

	Magic Vision - MKM Edition developed by Alexander Pick - Copyright 2017
	Original Magic Vision Created by Peter Simard - Copyright 2010

	This file is part of MKMTool
 
	MagicVision MKM Edition is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    MagicVision MKM Edition is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with MagicVision MKM Edition.  If not, see <http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von MagicVision MKM Edition.

    MagicVision MKM Edition ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Wahl) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.
    Fubar wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHRLEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.
    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System.Collections.Generic;
using System.Drawing;
using AForge;

namespace MKMEye
{
    internal class MagicCard
    {
        public Bitmap cardArtBitmap;
        public Bitmap cardBitmap;
        public List<IntPoint> corners;
        public ReferenceCard referenceCard;
    }
}