#!/usr/bin/env python3

import argparse
import os
import pathlib
import re
import shutil
import subprocess
from stat import S_IREAD, S_IRGRP, S_IROTH, S_IWUSR

ROOT_LIB = "/usr/local/lib/libgtk-3.dylib"
ADWAITA_THEME = "/usr/local/share/icons/Adwaita/index.theme"
PIXBUF_LOADERS = "/usr/local/lib/gdk-pixbuf-2.0"
OTOOL_LIB_REGEX = re.compile("(/usr/local/.*\.dylib)") # Ignore system libraries.

def collect_libs(src_lib, lib_deps):
    """
    Use otool -L to collect the library dependencies.
    """
    cmd = ['otool', '-L', src_lib]
    output = subprocess.check_output(cmd).decode('utf-8')
    referenced_paths = re.findall(OTOOL_LIB_REGEX, output)
    real_lib_paths = set([os.path.realpath(lib) for lib in referenced_paths])

    new_libs = []
    for lib in real_lib_paths:
        if lib not in lib_deps:
            new_libs.append(lib)
            lib_deps[lib] = referenced_paths

    for lib in new_libs:
        collect_libs(lib, lib_deps)

parser = argparse.ArgumentParser(description='Bundle the GTK libraries.')
parser.add_argument('--install_dir',
                    type=pathlib.Path,
                    required=True,
                    help='Directory to copy files to.')
args = parser.parse_args()

lib_deps = {}
collect_libs(ROOT_LIB, lib_deps)

for lib, deps in lib_deps.items():
    lib_copy = shutil.copy(lib, args.install_dir)
    # Make writable by user.
    os.chmod(lib_copy, S_IREAD | S_IRGRP | S_IROTH | S_IWUSR)

    # Run install_name_tool to fix up the absolute paths to the library
    # dependencies.
    for dep_path in deps:
        dep_lib = os.path.basename(os.path.realpath(dep_path))
        cmd = ['install_name_tool', '-change', dep_path, dep_lib, lib_copy]
        subprocess.check_output(cmd)

# Copy translations and icons.
gtk_root = os.path.join(os.path.dirname(os.path.realpath(ROOT_LIB)), "..")
shutil.copytree(os.path.join(gtk_root, 'share/locale'),
                os.path.join(args.install_dir, 'locale'),
                dirs_exist_ok=True)
shutil.copytree(os.path.join(gtk_root, 'share/icons'),
                os.path.join(args.install_dir, 'icons'),
                dirs_exist_ok=True)
adwaita_icons = os.path.join(os.path.dirname(os.path.realpath(ADWAITA_THEME)), "..")
shutil.copytree(adwaita_icons,
                os.path.join(args.install_dir, 'icons'),
                dirs_exist_ok=True)

# Copy pixbuf loaders
shutil.copytree(PIXBUF_LOADERS,
                os.path.join(args.install_dir, 'gdk-pixbuf-2.0'),
                dirs_exist_ok=True)
